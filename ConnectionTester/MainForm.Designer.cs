
namespace ConnectionTester
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBoxNetwork = new System.Windows.Forms.GroupBox();
			this.checkBoxDontFragment = new System.Windows.Forms.CheckBox();
			this.numericUpDownTTL = new System.Windows.Forms.NumericUpDown();
			this.labelTTL = new System.Windows.Forms.Label();
			this.numericUpDownPingTimeout = new System.Windows.Forms.NumericUpDown();
			this.labelNetworkFrequency = new System.Windows.Forms.Label();
			this.labelHostOrIP = new System.Windows.Forms.Label();
			this.textBoxHostOrIP = new System.Windows.Forms.TextBox();
			this.checkBoxNetwork = new System.Windows.Forms.CheckBox();
			this.buttonRunStop = new System.Windows.Forms.Button();
			this.checkBoxNetworkLink = new System.Windows.Forms.CheckBox();
			this.groupBoxNetworkLink = new System.Windows.Forms.GroupBox();
			this.buttonLinkFileName = new System.Windows.Forms.Button();
			this.labelLinkFileName = new System.Windows.Forms.Label();
			this.textBoxLinkFileName = new System.Windows.Forms.TextBox();
			this.numericUpDownNetworkLinkFrequency = new System.Windows.Forms.NumericUpDown();
			this.labelNetworkLinkFrequency = new System.Windows.Forms.Label();
			this.checkBoxSqlServer = new System.Windows.Forms.CheckBox();
			this.groupBoxSqlServer = new System.Windows.Forms.GroupBox();
			this.numericUpDownSqlServerFrequency = new System.Windows.Forms.NumericUpDown();
			this.labelSqlServerFrequency = new System.Windows.Forms.Label();
			this.labelDatabase = new System.Windows.Forms.Label();
			this.textBoxDatabase = new System.Windows.Forms.TextBox();
			this.labelPassword = new System.Windows.Forms.Label();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.textBoxLogin = new System.Windows.Forms.TextBox();
			this.labelLogin = new System.Windows.Forms.Label();
			this.labelServerAddress = new System.Windows.Forms.Label();
			this.textBoxServerAddress = new System.Windows.Forms.TextBox();
			this.listBoxLog = new System.Windows.Forms.ListBox();
			this.toolTipTTL = new System.Windows.Forms.ToolTip(this.components);
			this.toolTipDontFragment = new System.Windows.Forms.ToolTip(this.components);
			this.labelLogFileName = new System.Windows.Forms.Label();
			this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialogNetworkLink = new System.Windows.Forms.OpenFileDialog();
			this.textBoxLogFileName = new System.Windows.Forms.TextBox();
			this.buttonSetLogFile = new System.Windows.Forms.Button();
			this.checkBoxFileStream = new System.Windows.Forms.CheckBox();
			this.groupBoxFileStream = new System.Windows.Forms.GroupBox();
			this.radioButtonClient = new System.Windows.Forms.RadioButton();
			this.radioButtonServer = new System.Windows.Forms.RadioButton();
			this.numericUpDownFileStreamPort = new System.Windows.Forms.NumericUpDown();
			this.labelFileStreamPort = new System.Windows.Forms.Label();
			this.labelFileStreamHost = new System.Windows.Forms.Label();
			this.textBoxFileStreamHost = new System.Windows.Forms.TextBox();
			this.numericUpDownBatchSize = new System.Windows.Forms.NumericUpDown();
			this.labelBatchSize = new System.Windows.Forms.Label();
			this.groupBoxNetwork.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTTL)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingTimeout)).BeginInit();
			this.groupBoxNetworkLink.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownNetworkLinkFrequency)).BeginInit();
			this.groupBoxSqlServer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSqlServerFrequency)).BeginInit();
			this.groupBoxFileStream.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownFileStreamPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxNetwork
			// 
			this.groupBoxNetwork.Controls.Add(this.checkBoxDontFragment);
			this.groupBoxNetwork.Controls.Add(this.numericUpDownTTL);
			this.groupBoxNetwork.Controls.Add(this.labelTTL);
			this.groupBoxNetwork.Controls.Add(this.numericUpDownPingTimeout);
			this.groupBoxNetwork.Controls.Add(this.labelNetworkFrequency);
			this.groupBoxNetwork.Controls.Add(this.labelHostOrIP);
			this.groupBoxNetwork.Controls.Add(this.textBoxHostOrIP);
			resources.ApplyResources(this.groupBoxNetwork, "groupBoxNetwork");
			this.groupBoxNetwork.Name = "groupBoxNetwork";
			this.groupBoxNetwork.TabStop = false;
			// 
			// checkBoxDontFragment
			// 
			resources.ApplyResources(this.checkBoxDontFragment, "checkBoxDontFragment");
			this.checkBoxDontFragment.Checked = true;
			this.checkBoxDontFragment.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxDontFragment.Name = "checkBoxDontFragment";
			this.checkBoxDontFragment.UseVisualStyleBackColor = true;
			// 
			// numericUpDownTTL
			// 
			resources.ApplyResources(this.numericUpDownTTL, "numericUpDownTTL");
			this.numericUpDownTTL.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownTTL.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDownTTL.Name = "numericUpDownTTL";
			this.numericUpDownTTL.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
			// 
			// labelTTL
			// 
			resources.ApplyResources(this.labelTTL, "labelTTL");
			this.labelTTL.Name = "labelTTL";
			// 
			// numericUpDownPingTimeout
			// 
			resources.ApplyResources(this.numericUpDownPingTimeout, "numericUpDownPingTimeout");
			this.numericUpDownPingTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownPingTimeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDownPingTimeout.Name = "numericUpDownPingTimeout";
			this.numericUpDownPingTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// labelNetworkFrequency
			// 
			resources.ApplyResources(this.labelNetworkFrequency, "labelNetworkFrequency");
			this.labelNetworkFrequency.Name = "labelNetworkFrequency";
			// 
			// labelHostOrIP
			// 
			resources.ApplyResources(this.labelHostOrIP, "labelHostOrIP");
			this.labelHostOrIP.Name = "labelHostOrIP";
			// 
			// textBoxHostOrIP
			// 
			resources.ApplyResources(this.textBoxHostOrIP, "textBoxHostOrIP");
			this.textBoxHostOrIP.Name = "textBoxHostOrIP";
			// 
			// checkBoxNetwork
			// 
			resources.ApplyResources(this.checkBoxNetwork, "checkBoxNetwork");
			this.checkBoxNetwork.Name = "checkBoxNetwork";
			this.checkBoxNetwork.UseVisualStyleBackColor = true;
			// 
			// buttonRunStop
			// 
			resources.ApplyResources(this.buttonRunStop, "buttonRunStop");
			this.buttonRunStop.Name = "buttonRunStop";
			this.buttonRunStop.UseVisualStyleBackColor = true;
			this.buttonRunStop.Click += new System.EventHandler(this.buttonRunStop_Click);
			// 
			// checkBoxNetworkLink
			// 
			resources.ApplyResources(this.checkBoxNetworkLink, "checkBoxNetworkLink");
			this.checkBoxNetworkLink.Name = "checkBoxNetworkLink";
			this.checkBoxNetworkLink.UseVisualStyleBackColor = true;
			// 
			// groupBoxNetworkLink
			// 
			this.groupBoxNetworkLink.Controls.Add(this.numericUpDownBatchSize);
			this.groupBoxNetworkLink.Controls.Add(this.labelBatchSize);
			this.groupBoxNetworkLink.Controls.Add(this.buttonLinkFileName);
			this.groupBoxNetworkLink.Controls.Add(this.labelLinkFileName);
			this.groupBoxNetworkLink.Controls.Add(this.textBoxLinkFileName);
			this.groupBoxNetworkLink.Controls.Add(this.numericUpDownNetworkLinkFrequency);
			this.groupBoxNetworkLink.Controls.Add(this.labelNetworkLinkFrequency);
			resources.ApplyResources(this.groupBoxNetworkLink, "groupBoxNetworkLink");
			this.groupBoxNetworkLink.Name = "groupBoxNetworkLink";
			this.groupBoxNetworkLink.TabStop = false;
			// 
			// buttonLinkFileName
			// 
			resources.ApplyResources(this.buttonLinkFileName, "buttonLinkFileName");
			this.buttonLinkFileName.Name = "buttonLinkFileName";
			this.buttonLinkFileName.UseVisualStyleBackColor = true;
			this.buttonLinkFileName.Click += new System.EventHandler(this.buttonLinkFileName_Click);
			// 
			// labelLinkFileName
			// 
			resources.ApplyResources(this.labelLinkFileName, "labelLinkFileName");
			this.labelLinkFileName.Name = "labelLinkFileName";
			// 
			// textBoxLinkFileName
			// 
			resources.ApplyResources(this.textBoxLinkFileName, "textBoxLinkFileName");
			this.textBoxLinkFileName.Name = "textBoxLinkFileName";
			// 
			// numericUpDownNetworkLinkFrequency
			// 
			resources.ApplyResources(this.numericUpDownNetworkLinkFrequency, "numericUpDownNetworkLinkFrequency");
			this.numericUpDownNetworkLinkFrequency.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownNetworkLinkFrequency.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDownNetworkLinkFrequency.Name = "numericUpDownNetworkLinkFrequency";
			this.numericUpDownNetworkLinkFrequency.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// labelNetworkLinkFrequency
			// 
			resources.ApplyResources(this.labelNetworkLinkFrequency, "labelNetworkLinkFrequency");
			this.labelNetworkLinkFrequency.Name = "labelNetworkLinkFrequency";
			// 
			// checkBoxSqlServer
			// 
			resources.ApplyResources(this.checkBoxSqlServer, "checkBoxSqlServer");
			this.checkBoxSqlServer.Name = "checkBoxSqlServer";
			this.checkBoxSqlServer.UseVisualStyleBackColor = true;
			// 
			// groupBoxSqlServer
			// 
			this.groupBoxSqlServer.Controls.Add(this.numericUpDownSqlServerFrequency);
			this.groupBoxSqlServer.Controls.Add(this.labelSqlServerFrequency);
			this.groupBoxSqlServer.Controls.Add(this.labelDatabase);
			this.groupBoxSqlServer.Controls.Add(this.textBoxDatabase);
			this.groupBoxSqlServer.Controls.Add(this.labelPassword);
			this.groupBoxSqlServer.Controls.Add(this.textBoxPassword);
			this.groupBoxSqlServer.Controls.Add(this.textBoxLogin);
			this.groupBoxSqlServer.Controls.Add(this.labelLogin);
			this.groupBoxSqlServer.Controls.Add(this.labelServerAddress);
			this.groupBoxSqlServer.Controls.Add(this.textBoxServerAddress);
			resources.ApplyResources(this.groupBoxSqlServer, "groupBoxSqlServer");
			this.groupBoxSqlServer.Name = "groupBoxSqlServer";
			this.groupBoxSqlServer.TabStop = false;
			// 
			// numericUpDownSqlServerFrequency
			// 
			resources.ApplyResources(this.numericUpDownSqlServerFrequency, "numericUpDownSqlServerFrequency");
			this.numericUpDownSqlServerFrequency.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownSqlServerFrequency.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDownSqlServerFrequency.Name = "numericUpDownSqlServerFrequency";
			this.numericUpDownSqlServerFrequency.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// labelSqlServerFrequency
			// 
			resources.ApplyResources(this.labelSqlServerFrequency, "labelSqlServerFrequency");
			this.labelSqlServerFrequency.Name = "labelSqlServerFrequency";
			// 
			// labelDatabase
			// 
			resources.ApplyResources(this.labelDatabase, "labelDatabase");
			this.labelDatabase.Name = "labelDatabase";
			// 
			// textBoxDatabase
			// 
			resources.ApplyResources(this.textBoxDatabase, "textBoxDatabase");
			this.textBoxDatabase.Name = "textBoxDatabase";
			// 
			// labelPassword
			// 
			resources.ApplyResources(this.labelPassword, "labelPassword");
			this.labelPassword.Name = "labelPassword";
			// 
			// textBoxPassword
			// 
			resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
			this.textBoxPassword.Name = "textBoxPassword";
			// 
			// textBoxLogin
			// 
			resources.ApplyResources(this.textBoxLogin, "textBoxLogin");
			this.textBoxLogin.Name = "textBoxLogin";
			// 
			// labelLogin
			// 
			resources.ApplyResources(this.labelLogin, "labelLogin");
			this.labelLogin.Name = "labelLogin";
			// 
			// labelServerAddress
			// 
			resources.ApplyResources(this.labelServerAddress, "labelServerAddress");
			this.labelServerAddress.Name = "labelServerAddress";
			// 
			// textBoxServerAddress
			// 
			resources.ApplyResources(this.textBoxServerAddress, "textBoxServerAddress");
			this.textBoxServerAddress.Name = "textBoxServerAddress";
			// 
			// listBoxLog
			// 
			this.listBoxLog.BackColor = System.Drawing.SystemColors.Control;
			this.listBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.listBoxLog, "listBoxLog");
			this.listBoxLog.Name = "listBoxLog";
			// 
			// toolTipTTL
			// 
			this.toolTipTTL.ToolTipTitle = "Получает или задает число узлов маршрутизации, которые могут переадресовывать пак" +
    "ет Ping, прежде чем он будет удален.";
			// 
			// labelLogFileName
			// 
			resources.ApplyResources(this.labelLogFileName, "labelLogFileName");
			this.labelLogFileName.Name = "labelLogFileName";
			// 
			// saveFileDialogLog
			// 
			this.saveFileDialogLog.DefaultExt = "txt";
			resources.ApplyResources(this.saveFileDialogLog, "saveFileDialogLog");
			this.saveFileDialogLog.RestoreDirectory = true;
			// 
			// openFileDialogNetworkLink
			// 
			this.openFileDialogNetworkLink.FileName = "fileName";
			resources.ApplyResources(this.openFileDialogNetworkLink, "openFileDialogNetworkLink");
			this.openFileDialogNetworkLink.RestoreDirectory = true;
			// 
			// textBoxLogFileName
			// 
			resources.ApplyResources(this.textBoxLogFileName, "textBoxLogFileName");
			this.textBoxLogFileName.Name = "textBoxLogFileName";
			// 
			// buttonSetLogFile
			// 
			resources.ApplyResources(this.buttonSetLogFile, "buttonSetLogFile");
			this.buttonSetLogFile.Name = "buttonSetLogFile";
			this.buttonSetLogFile.UseVisualStyleBackColor = true;
			this.buttonSetLogFile.Click += new System.EventHandler(this.buttonSetLogFile_Click);
			// 
			// checkBoxFileStream
			// 
			resources.ApplyResources(this.checkBoxFileStream, "checkBoxFileStream");
			this.checkBoxFileStream.Name = "checkBoxFileStream";
			this.checkBoxFileStream.UseVisualStyleBackColor = true;
			// 
			// groupBoxFileStream
			// 
			this.groupBoxFileStream.Controls.Add(this.radioButtonClient);
			this.groupBoxFileStream.Controls.Add(this.radioButtonServer);
			this.groupBoxFileStream.Controls.Add(this.numericUpDownFileStreamPort);
			this.groupBoxFileStream.Controls.Add(this.labelFileStreamPort);
			this.groupBoxFileStream.Controls.Add(this.labelFileStreamHost);
			this.groupBoxFileStream.Controls.Add(this.textBoxFileStreamHost);
			resources.ApplyResources(this.groupBoxFileStream, "groupBoxFileStream");
			this.groupBoxFileStream.Name = "groupBoxFileStream";
			this.groupBoxFileStream.TabStop = false;
			// 
			// radioButtonClient
			// 
			resources.ApplyResources(this.radioButtonClient, "radioButtonClient");
			this.radioButtonClient.Checked = true;
			this.radioButtonClient.Name = "radioButtonClient";
			this.radioButtonClient.TabStop = true;
			this.radioButtonClient.UseVisualStyleBackColor = true;
			// 
			// radioButtonServer
			// 
			resources.ApplyResources(this.radioButtonServer, "radioButtonServer");
			this.radioButtonServer.Name = "radioButtonServer";
			this.radioButtonServer.UseVisualStyleBackColor = true;
			// 
			// numericUpDownFileStreamPort
			// 
			resources.ApplyResources(this.numericUpDownFileStreamPort, "numericUpDownFileStreamPort");
			this.numericUpDownFileStreamPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numericUpDownFileStreamPort.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
			this.numericUpDownFileStreamPort.Name = "numericUpDownFileStreamPort";
			this.numericUpDownFileStreamPort.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			// 
			// labelFileStreamPort
			// 
			resources.ApplyResources(this.labelFileStreamPort, "labelFileStreamPort");
			this.labelFileStreamPort.Name = "labelFileStreamPort";
			// 
			// labelFileStreamHost
			// 
			resources.ApplyResources(this.labelFileStreamHost, "labelFileStreamHost");
			this.labelFileStreamHost.Name = "labelFileStreamHost";
			// 
			// textBoxFileStreamHost
			// 
			resources.ApplyResources(this.textBoxFileStreamHost, "textBoxFileStreamHost");
			this.textBoxFileStreamHost.Name = "textBoxFileStreamHost";
			// 
			// numericUpDownBatchSize
			// 
			resources.ApplyResources(this.numericUpDownBatchSize, "numericUpDownBatchSize");
			this.numericUpDownBatchSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownBatchSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDownBatchSize.Name = "numericUpDownBatchSize";
			this.numericUpDownBatchSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// labelBatchSize
			// 
			resources.ApplyResources(this.labelBatchSize, "labelBatchSize");
			this.labelBatchSize.Name = "labelBatchSize";
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxFileStream);
			this.Controls.Add(this.groupBoxFileStream);
			this.Controls.Add(this.buttonSetLogFile);
			this.Controls.Add(this.textBoxLogFileName);
			this.Controls.Add(this.labelLogFileName);
			this.Controls.Add(this.listBoxLog);
			this.Controls.Add(this.groupBoxSqlServer);
			this.Controls.Add(this.checkBoxSqlServer);
			this.Controls.Add(this.groupBoxNetworkLink);
			this.Controls.Add(this.checkBoxNetworkLink);
			this.Controls.Add(this.buttonRunStop);
			this.Controls.Add(this.checkBoxNetwork);
			this.Controls.Add(this.groupBoxNetwork);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBoxNetwork.ResumeLayout(false);
			this.groupBoxNetwork.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownTTL)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPingTimeout)).EndInit();
			this.groupBoxNetworkLink.ResumeLayout(false);
			this.groupBoxNetworkLink.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownNetworkLinkFrequency)).EndInit();
			this.groupBoxSqlServer.ResumeLayout(false);
			this.groupBoxSqlServer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSqlServerFrequency)).EndInit();
			this.groupBoxFileStream.ResumeLayout(false);
			this.groupBoxFileStream.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownFileStreamPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxNetwork;
		private System.Windows.Forms.Label labelNetworkFrequency;
		private System.Windows.Forms.Label labelHostOrIP;
		private System.Windows.Forms.TextBox textBoxHostOrIP;
		private System.Windows.Forms.CheckBox checkBoxNetwork;
		private System.Windows.Forms.Button buttonRunStop;
		private System.Windows.Forms.CheckBox checkBoxNetworkLink;
		private System.Windows.Forms.GroupBox groupBoxNetworkLink;
		private System.Windows.Forms.NumericUpDown numericUpDownNetworkLinkFrequency;
		private System.Windows.Forms.Label labelNetworkLinkFrequency;
		private System.Windows.Forms.CheckBox checkBoxSqlServer;
		private System.Windows.Forms.GroupBox groupBoxSqlServer;
		private System.Windows.Forms.TextBox textBoxServerAddress;
		private System.Windows.Forms.Label labelServerAddress;
		private System.Windows.Forms.NumericUpDown numericUpDownPingTimeout;
		private System.Windows.Forms.NumericUpDown numericUpDownSqlServerFrequency;
		private System.Windows.Forms.Label labelSqlServerFrequency;
		private System.Windows.Forms.Label labelDatabase;
		private System.Windows.Forms.TextBox textBoxDatabase;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.TextBox textBoxLogin;
		private System.Windows.Forms.Label labelLogin;
		private System.Windows.Forms.ListBox listBoxLog;
		private System.Windows.Forms.NumericUpDown numericUpDownTTL;
		private System.Windows.Forms.Label labelTTL;
		private System.Windows.Forms.ToolTip toolTipTTL;
		private System.Windows.Forms.CheckBox checkBoxDontFragment;
		private System.Windows.Forms.ToolTip toolTipDontFragment;
		private System.Windows.Forms.Label labelLogFileName;
		private System.Windows.Forms.SaveFileDialog saveFileDialogLog;
		private System.Windows.Forms.OpenFileDialog openFileDialogNetworkLink;
		private System.Windows.Forms.TextBox textBoxLogFileName;
		private System.Windows.Forms.Button buttonSetLogFile;
		private System.Windows.Forms.Button buttonLinkFileName;
		private System.Windows.Forms.Label labelLinkFileName;
		private System.Windows.Forms.TextBox textBoxLinkFileName;
		private System.Windows.Forms.CheckBox checkBoxFileStream;
		private System.Windows.Forms.GroupBox groupBoxFileStream;
		private System.Windows.Forms.NumericUpDown numericUpDownFileStreamPort;
		private System.Windows.Forms.Label labelFileStreamPort;
		private System.Windows.Forms.Label labelFileStreamHost;
		private System.Windows.Forms.TextBox textBoxFileStreamHost;
		private System.Windows.Forms.RadioButton radioButtonClient;
		private System.Windows.Forms.RadioButton radioButtonServer;
		private System.Windows.Forms.NumericUpDown numericUpDownBatchSize;
		private System.Windows.Forms.Label labelBatchSize;
	}
}

