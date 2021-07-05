using System;
using System.Windows.Forms;

namespace ConnectionTester
{
	public partial class MainForm : Form
	{
		private readonly TestControllersCollection controllers = new TestControllersCollection();

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

			LoadConfiguration();

			toolTipTTL.SetToolTip(numericUpDownTTL, "Получает или задает число узлов маршрутизации, которые могут переадресовывать пакет Ping, прежде чем он будет удален.");

			toolTipDontFragment.SetToolTip(checkBoxDontFragment, "Установлена, если данные нельзя отправлять несколькими пакетами; в противном случае галка снята.");
		}

		private void LoadConfiguration()
		{
			var configuration = ConfigurationStorage.Load();

			checkBoxNetwork.Checked = configuration.Ping.State;
			textBoxHostOrIP.Text = configuration.Ping.HostOrIP;
			numericUpDownPingTimeout.Value = configuration.Ping.Timeout;
			numericUpDownTTL.Value = configuration.Ping.TTL;
			checkBoxDontFragment.Checked = configuration.Ping.DontFragment;

			checkBoxNetworkLink.Checked = configuration.NetworkLink.State;
			numericUpDownNetworkLinkFrequency.Value = configuration.NetworkLink.Frequency;
			textBoxLinkFileName.Text = configuration.NetworkLink.FileName;
			numericUpDownBatchSize.Value = configuration.NetworkLink.BatchSize;

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
		}

		private void Prepare()
		{
			Logger.SetScreen(listBoxLog);

			controllers.Add(
				new PingController(
					checkBoxNetwork,
					groupBoxNetwork,
					textBoxHostOrIP,
					numericUpDownPingTimeout,
					numericUpDownTTL,
					checkBoxDontFragment));

			controllers.Add(
				new NetworkLinkController(
					checkBoxNetworkLink,
					groupBoxNetworkLink,
					numericUpDownNetworkLinkFrequency,
					numericUpDownBatchSize,
					textBoxLinkFileName));

			controllers.Add(
				new SqlServerController(
					checkBoxSqlServer,
					groupBoxSqlServer,
					textBoxServerAddress,
					textBoxLogin,
					textBoxPassword,
					textBoxDatabase,
					numericUpDownSqlServerFrequency));

			controllers.Add(
				new FileStreamController(
					checkBoxFileStream,
					groupBoxFileStream,
					radioButtonServer,
					radioButtonClient,
					textBoxFileStreamHost,
					numericUpDownFileStreamPort));

			foreach (var controller in controllers)
				controller.Setup();
		}

		private void buttonRunStop_Click(object sender, EventArgs e)
		{
			if (controllers.Running)
				Stop();
			else
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
			if (controllers.Running)
				throw new InvalidOperationException(nameof(controllers.Running));

			if (controllers.HasAnyRunnedTest())
			{
				MessageBox.Show("Некоторые тесты всё ещё выполняются. Подождите немного и попробуйте ещё раз.", "Ожидание завершения задач", MessageBoxButtons.OK, MessageBoxIcon.Information);

				return;
			}

			buttonSetLogFile.Enabled = false;
			textBoxLogFileName.Enabled = false;

			if (!controllers.RunTest(Sender, textBoxLogFileName.Text.Trim()))
				return;

			buttonRunStop.Text = StopTest;
		}

		private void Stop()
		{
			if (!controllers.Running)
				return;

			controllers.StopTest(Sender);
			
			buttonRunStop.Text = RunTest;

			buttonSetLogFile.Enabled = true;
			textBoxLogFileName.Enabled = true;
		}

		private void SaveConfiguration()
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
			configuration.NetworkLink.BatchSize = (int)numericUpDownBatchSize.Value;

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

			SaveConfiguration();
		}
	}
}
