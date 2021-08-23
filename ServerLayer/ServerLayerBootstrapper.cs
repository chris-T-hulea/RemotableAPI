using Unity;
using Utils;

namespace ServerLayer
{
	public class ServerLayerBootstrapper : Bootstrapper
	{
		public ServerLayerBootstrapper(IUnityContainer container) : base(container)
		{
		}

		public override void Run()
		{
			this._container.RegisterType<IServer, Server>(TypeLifetime.Singleton);
		}
	}
}