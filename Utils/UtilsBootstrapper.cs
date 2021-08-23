using Unity;

namespace Utils
{
	public class UtilsBootstrapper : Bootstrapper
	{
		public UtilsBootstrapper(IUnityContainer container) : base(container)
		{
		}

		public override void Run()
		{
			this._container.RegisterSingleton<Constants>();
		}
	}
}