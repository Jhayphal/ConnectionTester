using System;
using System.Collections.Concurrent;
using System.IO;
using System.Windows.Forms;

namespace ConnectionTester
{
	static class Logger
	{
		public static ListBox ScreenLog { get; private set; }
		public static string LogFile { get; private set; }

		private static System.Timers.Timer timer = new System.Timers.Timer();
		private static ConcurrentQueue<string> log = new ConcurrentQueue<string>();

		public static void StartRecord(string logFileName)
		{
			LogFile = logFileName;

			timer.Interval = 10000;
			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Start();
		}

		public static void StopRecord()
		{
			timer.Stop();
			timer.Elapsed -= Timer_Elapsed;

			Flush();

			log = new ConcurrentQueue<string>();
		}

		private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Flush();
		}

		public static void Flush()
		{
			using (StreamWriter file = new StreamWriter(LogFile))
				while (log.TryDequeue(out string message))
					file.WriteLine(message);
		}

		public static void SetScreen(ListBox log)
		{
			ScreenLog = log;
		}

		public static void Write(string sender, string text)
		{
			WriteToFile(sender, text);
			WriteToScreen(sender, text);
		}

		public static void WriteToScreen(string sender, string text)
		{
			text = $"[{DateTime.Now:G}] [{sender}] {text}";

			ScreenLog.Invoke(new Action(() =>
			{
				ScreenLog.BeginUpdate();
				ScreenLog.Items.Add(text);
				ScreenLog.EndUpdate();
			}));
		}

		public static void WriteToFile(string sender, string text)
		{
			text = $"[{DateTime.Now:O}][{sender}] {text}";

			log.Enqueue(text);
		}
	}
}
