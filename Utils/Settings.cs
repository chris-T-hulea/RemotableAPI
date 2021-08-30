using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Configuration;

namespace Utils
{
	public class Settings
	{
		private readonly IConfigurationSection _config;

		public const string Name = nameof(Settings);

		public Settings(IConfigurationSection config)
		{
			_config = config;
		}

		public Settings()
		{
		}

		public string IP { get; set; }

		public int Port { get; set; }

		public bool DevMode { get; set; }
	}
}