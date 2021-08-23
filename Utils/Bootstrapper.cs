using Unity;

namespace Utils
{
	public abstract class Bootstrapper
	{
		protected readonly IUnityContainer _container;

		protected Bootstrapper(IUnityContainer container)
		{
			_container = container;
		}

		public abstract void Run();
	}
}