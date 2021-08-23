using Remotable.UiUtils;
using ServerLayer;
using ServiceLayer;
using Unity;
using Unity.Injection;
using Utils;

namespace Remotable
{
	public class UiBootstrapper : Bootstrapper
	{
		public UiBootstrapper(IUnityContainer container) : base(container)
		{
		}

		public override void Run()
		{
			this.RunExternalBootstrapper(this._container);
			this.RegisterInternalClasses(this._container);
		}

		private void RunExternalBootstrapper(IUnityContainer container)
		{
			container.Resolve<UtilsBootstrapper>().Run();
			container.Resolve<ServerLayerBootstrapper>().Run();
			container.Resolve<ServiceLayerBootstrapper>().Run();
		}

		private void RegisterInternalClasses(IUnityContainer container)
		{
			this.RegisterView<MainWindow, MainWindowViewModel>(container);
		}

		private void RegisterView<TView, TViewModel>(IUnityContainer container) where TViewModel : ViewModelBase
		{
			var viewModel = container.RegisterSingleton<TViewModel>();
			container.RegisterSingleton<TView>(new InjectionConstructor(viewModel));
		}
	}
}