using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using Utils;

namespace RestedApi
{
	public class MainBootstrapper : Bootstrapper

	{
		public MainBootstrapper(IServiceCollection container) : base(container)
		{
		}

		public override void Run()
		{
			new UtilsBootstrapper(_container).Run();
			new ServiceLayerBootstrapper(this._container).Run();
		}
	}
}