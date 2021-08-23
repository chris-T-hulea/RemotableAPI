using Unity;

namespace Utils
{
	public static class UnityContainerExtensions
	{
		public static T RegisterSingleton<T>(this IUnityContainer container)
		{
			container.RegisterType<T>(TypeLifetime.Singleton);
			return container.Resolve<T>();
		}

		public static TFrom RegisterSingleton<TFrom, TTo>(this IUnityContainer container) where TTo : TFrom
		{
			container.RegisterType<TFrom, TTo>(TypeLifetime.Singleton);
			return container.Resolve<TTo>();
		}
	}
}