using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Interfaces;
using Utils;

namespace ServiceLayer
{
	public class ServiceLayerBootstrapper : Bootstrapper
	{
		public ServiceLayerBootstrapper(IServiceCollection container) : base(container)
		{
		}

		public override void Run()
		{
			this._container.AddScoped<ILinkageService, LinkageService>();
			this._container.AddScoped<IControlService, ControlService>();
		}
	}
}