﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class FileStreamController : TestController
	{
		public readonly RadioButton Server;
		public readonly RadioButton Client;
		public readonly TextBox Host;
		public readonly NumericUpDown Port;

		public override string Sender => "Потоковая передача";

		public override bool Running => tokenSource != null; 

		const int BatchSize = 8;

		private volatile CancellationTokenSource tokenSource;
		private byte[] response = new byte[] { 0xFF, 0xFF, 0xFA, 0x01 };

		public FileStreamController(CheckBox state, GroupBox box, RadioButton server, RadioButton client, TextBox host, NumericUpDown port) : base(state, box)
		{
			Server = server ?? throw new ArgumentNullException(nameof(server));
			Client = client ?? throw new ArgumentNullException(nameof(client));
			Host = host ?? throw new ArgumentNullException(nameof(host));
			Port = port ?? throw new ArgumentNullException(nameof(port));

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

		public override bool Aviable(out string message)
		{
			if (Client.Checked && string.IsNullOrWhiteSpace(Host.Text))
			{
				message = "Не указан хост для FileStream.";

				return false;
			}

			message = string.Empty;

			return true;
		}

		public override bool Run()
		{
			if (Running)
				throw new InvalidOperationException(nameof(Running));

			if (!Aviable(out string _))
				return false;

			SetControlsState(enabled: false);

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
					listenSocket.SendTimeout = 120;
					listenSocket.ReceiveTimeout = 120;
					listenSocket.Bind(ipPoint);
					listenSocket.Listen(10);

					Logger.Write(Sender, "Сервер запущен. Ожидание подключений.");

					var generator = new Random(DateTime.Now.Millisecond);

					byte[] data = new byte[BatchSize];
					var responseLength = response.Length;
					var responseData = new byte[responseLength];

					while (!tokenSource.IsCancellationRequested)
					{
						Socket handler = listenSocket.Accept();

						Logger.Write(Sender, $"Подключен клиент {handler.RemoteEndPoint}.");

						while(handler.Connected)
						{
							generator.NextBytes(data);

							try
							{
								var sended = handler.Send(data);

								if (sended != data.Length)
									Logger.Write(Sender, $"Отправлено {sended} из {data.Length} байт.");

								var received = handler.Receive(responseData);

								if (received != responseLength)
									Logger.Write(Sender, $"Получено {received} из {responseLength} байт.");

								else if (!(responseData[0] == response[0]
									&& responseData[1] == response[1]
									&& responseData[2] == response[2]
									&& responseData[3] == response[3]))
									Logger.Write(Sender, "Ответ клиента отличается от ожидаемого.");

								Thread.Sleep(50);
							}
							catch(Exception e)
							{
								Logger.Write(Sender, e.Message);
							}

							if (tokenSource.IsCancellationRequested)
								break;
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
					socket.SendTimeout = 120;
					socket.ReceiveTimeout = 0;

					socket.Connect(ipPoint);

					Logger.Write(Sender, "Подключено.");

					byte[] data = new byte[BatchSize];

					var responseLength = response.Length;
					var responseData = new byte[responseLength];

					while (socket.Connected && !tokenSource.IsCancellationRequested)
					{	
						try
						{
							var received = socket.Receive(data);

							if (received != BatchSize)
								Logger.Write(Sender, $"Не удалось получить пакет либо его часть. Получено {received} из {BatchSize} байт.");

							var sended = socket.Send(response);

							if (sended != responseLength)
								Logger.Write(Sender, $"Отправлено {sended} из {responseLength} байт.");
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

		public override void Setup()
		{
			Box.Enabled = State.Checked;
			Host.Enabled = Client.Checked;

			Port.Minimum = 1024;
			Port.Maximum = 65535;
		}

		public override void Stop()
		{
			if (!Running)
				throw new InvalidOperationException(nameof(Running));

			tokenSource?.Cancel();

			SetControlsState(enabled: true);
		}
	}
}
