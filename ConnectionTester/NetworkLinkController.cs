using System;
using System.IO;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class NetworkLinkController : ITestController
	{
		public readonly CheckBox State;
		public readonly GroupBox Box;
		public readonly NumericUpDown Frequency;
		public readonly TextBox FileName;

		public string Sender => "Сетевой ярлык";

		public bool Enabled
		{
			get => State.Checked;
			set => State.Checked = value;
		}

		public NetworkLinkController(CheckBox state, GroupBox box, NumericUpDown frequency, TextBox fileName)
		{
			State = state;
			Box = box;
			Frequency = frequency;
			FileName = fileName;

			State.CheckedChanged += State_CheckedChanged;
		}

		private void State_CheckedChanged(object sender, EventArgs e)
		{
			Box.Enabled = State.Checked;
		}

		public bool Aviable(out string message)
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

		public bool Run()
		{
			throw new NotImplementedException();
		}

		public void Setup()
		{
			Frequency.Minimum = 50;
			Frequency.Maximum = 10000;

			Box.Enabled = State.Checked;
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}
	}
}
