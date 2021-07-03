using System;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class SqlServerController : ITestController
	{
		public readonly CheckBox State;
		public readonly GroupBox Box;
		public readonly TextBox Server;
		public readonly TextBox Login;
		public readonly TextBox Password;
		public readonly TextBox Database;
		public readonly NumericUpDown Frequency;

		public string Sender => "SQL";

		public bool Enabled
		{
			get => State.Checked;
			set => State.Checked = value;
		}

		public SqlServerController(CheckBox state, GroupBox box, TextBox server, TextBox login, TextBox password, TextBox database, NumericUpDown frequency)
		{
			State = state;
			Box = box;
			Server = server;
			Login = login;
			Password = password;
			Database = database;
			Frequency = frequency;

			State.CheckedChanged += State_CheckedChanged;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public bool Aviable(out string message)
		{
			message = string.Empty;

			if (string.IsNullOrWhiteSpace(Server.Text))
				message += " - адрес" + Environment.NewLine;

			if (string.IsNullOrWhiteSpace(Login.Text))
				message += " - логин" + Environment.NewLine;

			if (string.IsNullOrWhiteSpace(Password.Text))
				message += " - пароль" + Environment.NewLine;

			if (string.IsNullOrWhiteSpace(Database.Text))
				message += " - база данных";

			if (string.IsNullOrEmpty(message))
				return true;

			message = "Не заполнены данные подключения к SQL-серверу:" + Environment.NewLine + message.TrimEnd();

			return false;
		}

		public bool Run()
		{
			throw new NotImplementedException();
		}

		public void Setup()
		{
			Box.Enabled = State.Checked;

			Password.PasswordChar = '*';

			Frequency.Minimum = 10;
			Frequency.Maximum = 10000;
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}
	}
}
