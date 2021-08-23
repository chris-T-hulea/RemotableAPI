using Unity;
using Utils;

namespace ServiceLayer
{
	public class ServiceLayerBootstrapper : Bootstrapper
	{
		public ServiceLayerBootstrapper(IUnityContainer container) : base(container)
		{
		}

		public override void Run()
		{
			this._container.RegisterSingleton<LinkageService>();
		}
	}
}