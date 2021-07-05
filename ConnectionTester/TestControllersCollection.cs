using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ConnectionTester
{
	sealed class TestControllersCollection : ICollection<TestController>
	{
		public int Count => controllers?.Count ?? 0;

		public bool IsReadOnly => false;

		public bool Running { get; private set; }

		private readonly List<TestController> controllers = new List<TestController>();

		public void Add(TestController item)
		{
			controllers.Add(item);
		}

		public void Clear()
		{
			controllers.Clear();
		}

		public bool Contains(TestController item)
		{
			return controllers.Contains(item);
		}

		public void CopyTo(TestController[] array, int arrayIndex)
		{
			controllers.CopyTo(array, arrayIndex);
		}

		public IEnumerator<TestController> GetEnumerator()
		{
			return controllers.GetEnumerator();
		}

		public bool Remove(TestController item)
		{
			return controllers.Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return controllers.GetEnumerator();
		}

		public bool HasAnyRunnedTest()
		{
			return controllers.Any(x => x.Running);
		}

		public bool RunTest(string sender, string logFileName)
		{
			if (Running)
				throw new InvalidOperationException(nameof(Running));

			Logger.StartRecord(logFileName);

			string errors = string.Empty;

			foreach (var controller in controllers)
				if (controller.Enabled)
					if (!controller.Aviable(out string message))
						errors += message + Environment.NewLine;

			errors = errors.TrimEnd();

			if (!string.IsNullOrWhiteSpace(errors))
			{
				Logger.WriteToScreen(sender, "Недостаточно данных.");

				MessageBox.Show(errors, "Недостаточно данных", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}

			Logger.Write(sender, "Запуск тестирования...");


			foreach (var controller in controllers)
				if (controller.Enabled)
					controller.Run();
				else
					controller.SetControlsState(enabled: false);

			Running = true;

			return true;
		}

		public void StopTest(string sender)
		{
			if (!Running)
				throw new InvalidOperationException();

			Logger.Write(sender, "Остановка тестирования...");

			foreach (var controller in controllers)
				if (controller.Enabled)
					controller.Stop();
				else
					controller.SetControlsState(enabled: true);

			Running = false;

			Logger.StopRecord();
		}
	}
}
