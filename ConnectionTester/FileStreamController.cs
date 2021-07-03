using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class FileStreamController : ITestController
	{
		public readonly CheckBox State;
		public readonly GroupBox Box;
		public readonly RadioButton Server;
		public readonly RadioButton Client;
		public readonly TextBox Host;
		public readonly NumericUpDown Port;

		public string Sender => "File stream";

		const int BatchSize = 1024;

		private CancellationTokenSource tokenSource;

		public bool Enabled
		{
			get => State.Checked;
			set => State.Checked = value;
		}

		public FileStreamController(CheckBox state, GroupBox box, RadioButton server, RadioButton client, TextBox host, NumericUpDown port)
		{
			State = state;
			Box = box;
			Server = server;
			Client = client;
			Host = host;
			Port = port;

			State.CheckedChanged += State_CheckedChanged;
			Client.CheckedChanged += Client_CheckedChanged;
		}

		private void Client_CheckedChanged(object sender, EventArgs e)
		{
			Host.Enabled = Client.Checked;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public bool Aviable(out string message)
		{
			if (Client.Checked && string.IsNullOrWhiteSpace(Host.Text))
			{
				message = "Не указан хост для FileStream.";

				return false;
			}

			message = string.Empty;

			return true;
		}

		public bool Run()
		{
			if (!Aviable(out string _))
				return false;

			State.Enabled = false;
			Box.Enabled = false;

			tokenSource?.Cancel();

			tokenSource = new CancellationTokenSource();

			if (Client.Checked)
				Task.Run(ConnectToHost);
			else
				Task.Run(StartServer);

			return true;
		}

		private void StartServer()
		{
			var port = (int)Port.Value;
			var host = Dns.GetHostName();
			var ips = Dns.GetHostAddresses(host);
			var currentIp = ips.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

			IPEndPoint ipPoint = new IPEndPoint(currentIp, port);

			Logger.Write(Sender, $"Запуск сервера по адресу {ipPoint}.");

			try
			{
				using (Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
					listenSocket.ReceiveTimeout = 1000;
					listenSocket.Bind(ipPoint);
					listenSocket.Listen(10);

					Logger.Write(Sender, "Сервер запущен. Ожидание подключений.");

					while (!tokenSource.IsCancellationRequested)
					{
						Socket handler = listenSocket.Accept();

						Logger.Write(Sender, $"Подключен клиент {handler.RemoteEndPoint}.");

						byte[] data = new byte[BatchSize];

						while(handler.Connected)
						{
							if (handler.Available > 0)
								handler.Receive(data);
						}

						Logger.Write(Sender, $"Клиент {handler.RemoteEndPoint} закрыл соединение.");
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(Sender, ex.Message);
			}
			finally
			{
				Logger.Write(Sender, "Сервер остановлен.");
			}
		}

		private void ConnectToHost()
		{
			try
			{
				var host = Host.Text.Trim();
				var port = (int)Port.Value;

				Logger.Write(Sender, $"Подключение к серверу {host}:{port}.");

				IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(host), port);

				using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
					socket.SendTimeout = 1000;

					socket.Connect(ipPoint);

					Logger.Write(Sender, "Подключено.");

					var generator = new Random(DateTime.Now.Millisecond);

					byte[] data = new byte[BatchSize];

					while (socket.Connected && !tokenSource.IsCancellationRequested)
					{
						generator.NextBytes(data);

						try
						{
							var sended = socket.Send(data);

							if (sended != BatchSize)
								Logger.Write(Sender, $"Не удалось доставить пакет либо его часть. Отправлено {sended} из {BatchSize} байт.");
						}
						catch(Exception e)
						{
							Logger.Write(Sender, e.Message);
						}
					}

					tokenSource = null;

					socket.Shutdown(SocketShutdown.Both);
					socket.Disconnect(reuseSocket: false);
				}
			}
			catch (Exception ex)
			{
				Logger.Write(Sender, ex.Message);
			}
			finally
			{
				Logger.Write(Sender, "Соединение закрыто.");
			}
		}

		public void Setup()
		{
			Box.Enabled = State.Checked;
			Host.Enabled = Client.Checked;

			Port.Minimum = 1024;
			Port.Maximum = 65535;
		}

		public void Stop()
		{
			tokenSource?.Cancel();

			State.Enabled = true;
			Box.Enabled = State.Checked;
		}
	}
}
