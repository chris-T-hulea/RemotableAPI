
namespace Model
{
	public class Settings
	{
		public const string Name = nameof(Settings);


		public Settings()
		{
		}

		public string IP { get; set; }

		public int Port { get; set; }

		public bool DevMode { get; set; }
	}
}