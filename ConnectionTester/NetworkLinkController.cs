using System;
using System.IO;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class NetworkLinkController : TestController
	{
		public readonly NumericUpDown Frequency;
		public readonly TextBox FileName;
		public readonly NumericUpDown BatchSize;

		public override string Sender => "Сетевой ярлык";

		public override bool Running => timer != null;

		private System.Timers.Timer timer;

		public NetworkLinkController(CheckBox state, GroupBox box, NumericUpDown frequency, NumericUpDown batchSize, TextBox fileName) : base(state, box)
		{
			Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency));
			BatchSize = batchSize ?? throw new ArgumentNullException(nameof(batchSize));
			FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));

			State.CheckedChanged += State_CheckedChanged;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public override bool Aviable(out string message)
		{
			if (string.IsNullOrWhiteSpace(FileName.Text))
			{
				message = "Не указано имя файла в режиме сетевого ярлыка.";

				return false;
			}

			if (!File.Exists(FileName.Text))
			{
				message = "Указанный для сетевого ярлыка файл не существует.";

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

			var frequency = (int)Frequency.Value;

			Logger.Write(Sender, $"Запуск. Файл {FileName.Text.Trim()}. Частота чтения {frequency} мс.");

			timer = new System.Timers.Timer
			{
				Interval = frequency
			};

			timer.Elapsed += Timer_Elapsed;
			timer.Start();

			Logger.Write(Sender, $"Запущено.");

			return true;
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			timer.Enabled = false;

			var fileName = FileName.Text.Trim();
			var batchSize = (int)BatchSize.Value;

			try
			{
				using (FileStream file = new FileStream(fileName, FileMode.Open))
				{
					var length = file.Length;

					if (length == 0)
					{
						Logger.Write(Sender, $"Файл пуст.");

						return;
					}

					if (length < batchSize)
						Logger.Write(Sender, $"Размер файла меньше, чем количество байт для чтения. Будет прочитан весь файл.");

					var bytes = new byte[batchSize];
					var readed = file.Read(bytes, 0, batchSize);

					if (readed != batchSize)
						Logger.Write(Sender, $"Прочитано {readed} из {BatchSize} байт.");
				}
			}
			catch(FileNotFoundException)
			{
				Logger.Write(Sender, "Не найден целевой файл.");
			}
			catch(Exception ex)
			{
				Logger.Write(Sender, ex.Message);
			}
			finally
			{
				timer.Enabled = true;
			}
		}

		public override void Setup()
		{
			Frequency.Minimum = 50;
			Frequency.Maximum = 10000;

			BatchSize.Minimum = 1024;
			BatchSize.Maximum = 1024 * 1024;

			Box.Enabled = State.Checked;
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

			Logger.Write(Sender, "Задача остановлена.");
		}
	}
}
