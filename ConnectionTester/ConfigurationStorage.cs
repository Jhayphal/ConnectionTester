using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ConnectionTester
{
	[Serializable]
	public sealed class ConfigurationStorage
	{
		private const string FileName = "config.bin";

		public PingConfiguration Ping { get; set; }

		public NetworkLinkConfiguration NetworkLink { get; set; }

		public SqlServerConfiguration SqlServer { get; set; }

		public FileStreamConfiguration FileStream { get; set; }

		public string LogFileName { get; set; } = "log.txt";

		public ConfigurationStorage()
		{
			Ping = new PingConfiguration();
			NetworkLink = new NetworkLinkConfiguration();
			SqlServer = new SqlServerConfiguration();
			FileStream = new FileStreamConfiguration();
		}

		public void Save()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(GetType());

				using (var file = new FileStream(FileName, FileMode.Create))
				using (var security = new SecurityStream(file))
				using (var writer = new StreamWriter(security))
					serializer.Serialize(writer, this);
			}
			catch (Exception e)
			{
				MessageBox.Show("Не удалось сохранить изменения параметров. " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static ConfigurationStorage Load()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationStorage));

				using (var file = new FileStream(FileName, FileMode.Open, FileAccess.Read))
				using (var security = new SecurityStream(file))
				using (var reader = new StreamReader(security))
					return (ConfigurationStorage)serializer.Deserialize(reader);
			}
			catch
			{
				return new ConfigurationStorage();
			}
		}
	}

	[Serializable]
	public sealed class PingConfiguration
	{
		public bool State { get; set; }
		public string HostOrIP { get; set; } = string.Empty;
		public int Timeout { get; set; } = 1000;
		public int TTL { get; set; } = 128;
		public bool DontFragment { get; set; } = true;
	}

	[Serializable]
	public sealed class NetworkLinkConfiguration
	{
		public bool State { get; set; }
		public int Frequency { get; set; } = 1000;
		public string FileName { get; set; } = string.Empty;
	}

	[Serializable]
	public sealed class SqlServerConfiguration
	{
		public bool State { get; set; }
		public string ServerAddress { get; set; } = string.Empty;
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Database { get; set; } = string.Empty;
		public int Frequency { get; set; } = 1000;
	}

	[Serializable]
	public sealed class FileStreamConfiguration
	{
		public bool State { get; set; }
		public bool ServerMode { get; set; }
		public string Host { get; set; } = string.Empty;
		public int Port { get; set; } = 9999;
	}
}
