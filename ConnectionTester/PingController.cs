using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class PingController : ITestController
	{
		public readonly CheckBox State;
		public readonly GroupBox Box;
		public readonly TextBox HostOrIP;
		public readonly NumericUpDown Timeout;
		public readonly NumericUpDown Ttl;
		public readonly CheckBox DontFragment;

		public bool Enabled
		{
			get => State.Checked;
			set => State.Checked = value;
		}

		public string Sender => "Ping";

		private volatile CancellationTokenSource tokenSource;

		public PingController(CheckBox state, GroupBox box, TextBox hostOrIP, NumericUpDown timeout, NumericUpDown ttl, CheckBox dontFragment)
		{
			State = state;
			Box = box;
			HostOrIP = hostOrIP;
			Timeout = timeout;
			Ttl = ttl;
			DontFragment = dontFragment;

			State.CheckedChanged += State_CheckedChanged;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public bool Run()
		{
			if (!Aviable(out string _))
				return false;

			State.Enabled = false;
			Box.Enabled = false;

			tokenSource?.Cancel();

			tokenSource = new CancellationTokenSource();

			Task.Run(Ping);

			return true;
		}

		private void Ping()
		{
			Logger.Write(Sender, "Запуск...");

			string who = HostOrIP.Text.Trim();

			Ping pingSender = new Ping();
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(data);

			int timeout = (int)Timeout.Value;
			int ttl = (int)Ttl.Value;
			bool dontFragment = DontFragment.Checked;

			PingOptions options = new PingOptions(ttl, dontFragment);

			Logger.Write(Sender, $"Параметры: хост - {who}, таймаут - {timeout}, время жизни - {ttl}, фрагментация - {(dontFragment ? "отключена" : "включена")}.");

			while (!tokenSource.IsCancellationRequested)
			{
				try
				{
					var result = pingSender.Send(who, timeout, buffer, options);

					if (result.Status != IPStatus.Success)
						LogMessage(result, who);
				}
				catch (Exception e)
				{
					Logger.Write(Sender, e.Message);

					break;
				}
			}

			Logger.Write(Sender, "Остановлен.");

			tokenSource = null;
		}

		private void LogMessage(PingReply reply, string host)
		{
			var message = $"Host: {host}; Status: {reply.Status}.";

			Logger.Write(Sender, message);
		}

		public void Setup()
		{
			Box.Enabled = State.Checked;

			Timeout.Minimum = 10;
			Timeout.Maximum = 10000;

			Ttl.Minimum = 1;
			Ttl.Maximum = 1024;
		}

		public void Stop()
		{
			tokenSource?.Cancel();

			State.Enabled = true;
			Box.Enabled = State.Checked;
		}

		public bool Aviable(out string message)
		{
			if (string.IsNullOrWhiteSpace(HostOrIP.Text))
			{
				message = "Не указан хост или IP-адрес для Ping.";

				return false;
			}

			message = string.Empty;

			return true;
		}
	}
}
