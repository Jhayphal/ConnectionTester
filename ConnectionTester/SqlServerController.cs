using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class SqlServerController : TestController
	{
		public readonly TextBox Server;
		public readonly TextBox Login;
		public readonly TextBox Password;
		public readonly TextBox Database;
		public readonly NumericUpDown Frequency;

		public override string Sender => "SQL Server";

		public override bool Running => timer != null;

		private System.Timers.Timer timer;

		public SqlServerController(CheckBox state, GroupBox box, TextBox server, TextBox login, TextBox password, TextBox database, NumericUpDown frequency) : base(state, box)
		{
			Server = server ?? throw new ArgumentNullException(nameof(server));
			Login = login ?? throw new ArgumentNullException(nameof(login));
			Password = password ?? throw new ArgumentNullException(nameof(password));
			Database = database ?? throw new ArgumentNullException(nameof(database));
			Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency));

			State.CheckedChanged += State_CheckedChanged;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public override bool Aviable(out string message)
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

		public override bool Run()
		{
			if (Running)
				throw new InvalidOperationException(nameof(Running));

			if (!Aviable(out string _))
				return false;

			SetControlsState(enabled: false);

			Logger.Write(Sender, $"Запуск. Сервер: {Server.Text.Trim()}. База данных: {Database.Text.Trim()}.");

			var frequency = (int)Frequency.Value;

			timer = new System.Timers.Timer
			{
				Interval = frequency
			};

			timer.Elapsed += Timer_Elapsed;
			timer.Start();

			Logger.Write(Sender, "Запущено.");

			return true;
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			timer.Enabled = false;

			try
			{
				SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
				{
					InitialCatalog = Database.Text.Trim(),
					DataSource = Server.Text.Trim(),
					IntegratedSecurity = false,
					ConnectTimeout = (int)Frequency.Value,
					UserID = Login.Text.Trim(),
					Password = Password.Text.Trim()
				};

				using (SqlCommand request = new SqlCommand("SELECT 1"))
				{
					using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
					{
						request.Connection = connection;

						object value = request.ExecuteScalar();

						if (value == null || value == DBNull.Value)
							Logger.Write(Sender, "Не удалось получить значение.");

						else if (!(value is int i))
							Logger.Write(Sender, $"Тип ({value.GetType()}) полученного значения ({value}) отличается от ожидаемого (int).");

						else if (i != 1)
							Logger.Write(Sender, $"Полученный ответ {i} не совпадает с ожидаемым (1).");
					}
				}
			}
			catch(Exception ex)
			{
				Logger.Write(Sender, ex.Message);
			}

			timer.Enabled = true;
		}

		public override void Setup()
		{
			Box.Enabled = State.Checked;

			Password.PasswordChar = '*';

			Frequency.Minimum = 10;
			Frequency.Maximum = 10000;
		}

		public override void Stop()
		{
			if (!Running)
				throw new InvalidOperationException(nameof(Running));

			if (timer != null)
			{
				timer.Stop();
				timer.Elapsed -= Timer_Elapsed;
				timer.Dispose();

				timer = null;
			}

			SetControlsState(enabled: true);

			Logger.Write(Sender, $"Остановлено.");
		}
	}
}
