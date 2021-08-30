using Microsoft.Extensions.DependencyInjection;

namespace Utils
{
	public class UtilsBootstrapper : Bootstrapper
	{
		public UtilsBootstrapper(IServiceCollection container) : base(container)
		{
		}

		public override void Run()
		{
			//this._container.AddScoped<Settings>();
		}
	}
}