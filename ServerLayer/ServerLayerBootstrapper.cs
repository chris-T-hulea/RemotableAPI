using Microsoft.Extensions.DependencyInjection;
using ServerLayer.Interfaces;
using Utils;

namespace ServerLayer
{
	public class ServerLayerBootstrapper : Bootstrapper
	{
		public ServerLayerBootstrapper(IServiceCollection container) : base(container)
		{
		}

		public override void Run()
		{
			this._container.AddScoped<IServer, Server>();
		}
	}
}