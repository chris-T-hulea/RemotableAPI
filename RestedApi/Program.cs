using System.Data;
using System.Text.Json;
using WindowsInput.Native;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Model.Entities;

namespace RestedApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.ConfigureWebHostDefaults(webBuilder =>
						{
#if !DEBUG
							webBuilder.UseKestrel(options => options.Listen(IPAddress.Parse("192.168.1.106"), 8080));
#endif
							webBuilder.UseStartup<Startup>();
						});
	}
}