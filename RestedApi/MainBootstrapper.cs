using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using Model;

namespace RestedApi
{
	public class MainBootstrapper
	{
		public void Run(IServiceCollection container)
		{
			new ServiceLayerBootstrapper().Run(container);
		}
	}
}