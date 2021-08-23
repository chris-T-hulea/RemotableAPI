using System.Collections.Specialized;
using System.Configuration;

namespace Utils
{
	public class Constants
	{
		private readonly NameValueCollection _settings;

		public Constants()
		{
			this._settings = ConfigurationManager.AppSettings;
		}

		public string Url => _settings[nameof(Url)];

		public bool DevMode => bool.Parse(this._settings[nameof(DevMode)]);
	}
}