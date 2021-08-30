using System.Diagnostics;

namespace Model.Entities
{
	public class App
	{
		public App()
		{
		}

		public App(Process process)
		{
			this.Id = process.Id;
			this.Name = process.ProcessName;
			this.Title = process.MainWindowTitle;
			this.Process = process;
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }

		public Process Process { get; set; }
	}
}