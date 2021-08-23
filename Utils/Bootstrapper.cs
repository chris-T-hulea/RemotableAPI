using Microsoft.Extensions.DependencyInjection;

namespace Utils
{
	public abstract class Bootstrapper
	{
		protected readonly IServiceCollection _container;

		protected Bootstrapper(IServiceCollection container)
		{
			_container = container;
		}

		public abstract void Run();
	}
}