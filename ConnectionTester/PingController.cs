using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class PingController : TestController
	{
		public readonly TextBox HostOrIP;
		public readonly NumericUpDown Timeout;
		public readonly NumericUpDown Ttl;
		public readonly CheckBox DontFragment;

		public override string Sender => "Ping";

		public override bool Running => tokenSource != null;

		private volatile CancellationTokenSource tokenSource;

		public PingController(CheckBox state, GroupBox box, TextBox hostOrIP, NumericUpDown timeout, NumericUpDown ttl, CheckBox dontFragment) : base(state, box)
		{
			HostOrIP = hostOrIP ?? throw new ArgumentNullException(nameof(hostOrIP));
			Timeout = timeout ?? throw new ArgumentNullException(nameof(timeout));
			Ttl = ttl ?? throw new ArgumentNullException(nameof(ttl));
			DontFragment = dontFragment ?? throw new ArgumentNullException(nameof(dontFragment));

			State.CheckedChanged += State_CheckedChanged;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public override bool Run()
		{
			if (Running)
				throw new InvalidOperationException(nameof(Running));

			if (!Aviable(out string _))
				return false;

			Logger.Write(Sender, "Запуск.");

			SetControlsState(enabled: false);

			tokenSource?.Cancel();

			tokenSource = new CancellationTokenSource();

			Task.Run(Ping);

			return true;
		}

		private void Ping()
		{
			string who = HostOrIP.Text.Trim();

			Ping pingSender = new Ping();
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(data);

			int timeout = (int)Timeout.Value;
			int ttl = (int)Ttl.Value;
			bool dontFragment = DontFragment.Checked;

			PingOptions options = new PingOptions(ttl, dontFragment);

			Logger.Write(Sender, $"Запущено. Хост {who}. Таймаут {timeout}. Время жизни {ttl}. Фрагментация {(dontFragment ? "отключена" : "включена")}.");

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

			Logger.Write(Sender, "Остановлено.");

			tokenSource = null;
		}

		private void LogMessage(PingReply reply, string host)
		{
			var message = $"Host: {host}; Status: {reply.Status}.";

			Logger.Write(Sender, message);
		}

		public override void Setup()
		{
			Box.Enabled = State.Checked;

			Timeout.Minimum = 10;
			Timeout.Maximum = 10000;

			Ttl.Minimum = 1;
			Ttl.Maximum = 1024;
		}

		public override void Stop()
		{
			if (!Running)
				throw new InvalidOperationException(nameof(Running));

			tokenSource?.Cancel();

			SetControlsState(enabled: true);
		}

		public override bool Aviable(out string message)
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
