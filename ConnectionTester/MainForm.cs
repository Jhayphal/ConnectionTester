using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConnectionTester
{
	public partial class MainForm : Form
	{
		List<ITestController> controllers;
		bool running = false;

		const string Sender = "Главное окно";

		const string RunTest = "Запустить тестирование";
		const string StopTest = "Остановить тестирование";

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			InitControls();

			Prepare();
		}

		void InitControls()
		{
			buttonRunStop.Text = RunTest;

			var configuration = ConfigurationStorage.Load();

			checkBoxNetwork.Checked = configuration.Ping.State;
			textBoxHostOrIP.Text = configuration.Ping.HostOrIP;
			numericUpDownPingTimeout.Value = configuration.Ping.Timeout;
			numericUpDownTTL.Value = configuration.Ping.TTL;
			checkBoxDontFragment.Checked = configuration.Ping.DontFragment;

			checkBoxNetworkLink.Checked = configuration.NetworkLink.State;
			numericUpDownNetworkLinkFrequency.Value = configuration.NetworkLink.Frequency;
			textBoxLinkFileName.Text = configuration.NetworkLink.FileName;

			checkBoxSqlServer.Checked = configuration.SqlServer.State;
			textBoxServerAddress.Text = configuration.SqlServer.ServerAddress;
			textBoxLogin.Text = configuration.SqlServer.Login;
			textBoxPassword.Text = configuration.SqlServer.Password;
			textBoxDatabase.Text = configuration.SqlServer.Database;
			numericUpDownSqlServerFrequency.Value = configuration.SqlServer.Frequency;

			checkBoxFileStream.Checked = configuration.FileStream.State;
			radioButtonServer.Checked = configuration.FileStream.ServerMode;
			textBoxFileStreamHost.Text = configuration.FileStream.Host;
			numericUpDownFileStreamPort.Value = configuration.FileStream.Port;

			textBoxLogFileName.Text = configuration.LogFileName;

			toolTipTTL.SetToolTip(numericUpDownTTL, "Получает или задает число узлов маршрутизации, которые могут переадресовывать пакет Ping, прежде чем он будет удален.");

			toolTipDontFragment.SetToolTip(checkBoxDontFragment, "Установлена, если данные нельзя отправлять несколькими пакетами; в противном случае галка снята.");
		}

		private void Prepare()
		{
			Logger.SetScreen(listBoxLog);

			controllers = new List<ITestController>
			{
				new PingController(
					checkBoxNetwork,
					groupBoxNetwork,
					textBoxHostOrIP,
					numericUpDownPingTimeout,
					numericUpDownTTL,
					checkBoxDontFragment),
				new NetworkLinkController(
					checkBoxNetworkLink,
					groupBoxNetworkLink,
					numericUpDownNetworkLinkFrequency,
					textBoxLinkFileName),
				new SqlServerController(
					checkBoxSqlServer,
					groupBoxSqlServer,
					textBoxServerAddress,
					textBoxLogin,
					textBoxPassword,
					textBoxDatabase,
					numericUpDownSqlServerFrequency),
				new FileStreamController(
					checkBoxFileStream,
					groupBoxFileStream,
					radioButtonServer,
					radioButtonClient,
					textBoxFileStreamHost,
					numericUpDownFileStreamPort)
			};

			foreach (var controller in controllers)
				controller.Setup();
		}

		private void buttonRunStop_Click(object sender, EventArgs e)
		{
			if (running)
			{
				Stop();

				return;
			}

			Start();
		}

		private void buttonSetLogFile_Click(object sender, EventArgs e)
		{
			if (saveFileDialogLog.ShowDialog() == DialogResult.OK)
				textBoxLogFileName.Text = saveFileDialogLog.FileName;
		}

		private void buttonLinkFileName_Click(object sender, EventArgs e)
		{
			if (openFileDialogNetworkLink.ShowDialog() == DialogResult.OK)
				textBoxLinkFileName.Text = openFileDialogNetworkLink.FileName;
		}

		private void Start()
		{
			if (running)
				throw new InvalidOperationException(nameof(running));

			Logger.StartRecord(textBoxLogFileName.Text);

			Logger.Write(Sender, "Запуск тестирования...");

			string errors = string.Empty;

			foreach (var controller in controllers)
				if (controller.Enabled)
					if (!controller.Aviable(out string message))
						errors += message + Environment.NewLine;

			errors = errors.TrimEnd();

			if (!string.IsNullOrWhiteSpace(errors))
			{
				MessageBox.Show(errors, "Недостаточно данных", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			}

			running = true;

			buttonRunStop.Text = StopTest;

			foreach (var controller in controllers)
				if (controller.Enabled)
					controller.Run();
		}

		private void Stop()
		{
			if (!running)
				return;

			Logger.Write(Sender, "Остановка тестирования...");

			foreach (var controller in controllers)
				if (controller.Enabled)
					controller.Stop();

			running = false;

			buttonRunStop.Text = RunTest;

			Logger.StopRecord();
		}

		private void SyncConfig()
		{
			var configuration = new ConfigurationStorage();

			configuration.Ping.State = checkBoxNetwork.Checked;
			configuration.Ping.HostOrIP = textBoxHostOrIP.Text;
			configuration.Ping.Timeout = (int)numericUpDownPingTimeout.Value;
			configuration.Ping.TTL = (int)numericUpDownTTL.Value;
			configuration.Ping.DontFragment = checkBoxDontFragment.Checked;

			configuration.NetworkLink.State = checkBoxNetworkLink.Checked;
			configuration.NetworkLink.Frequency = (int)numericUpDownNetworkLinkFrequency.Value;
			configuration.NetworkLink.FileName = textBoxLinkFileName.Text;

			configuration.SqlServer.State = checkBoxSqlServer.Checked;
			configuration.SqlServer.ServerAddress = textBoxServerAddress.Text;
			configuration.SqlServer.Login = textBoxLogin.Text;
			configuration.SqlServer.Password = textBoxPassword.Text;
			configuration.SqlServer.Database = textBoxDatabase.Text;
			configuration.SqlServer.Frequency = (int)numericUpDownSqlServerFrequency.Value;

			configuration.FileStream.State = checkBoxFileStream.Checked;
			configuration.FileStream.ServerMode = radioButtonServer.Checked;
			configuration.FileStream.Host = textBoxFileStreamHost.Text;
			configuration.FileStream.Port = (int)numericUpDownFileStreamPort.Value;

			configuration.LogFileName = textBoxLogFileName.Text;

			configuration.Save();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Stop();

			SyncConfig();
		}
	}
}
