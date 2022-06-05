
using RemoteR.Utils;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.DTO;
using Model;

namespace RemoteR
{
	public partial class MainPageModel : BaseModel
	{
		private const double VolumeIncrement = 10;
		private Task<double> volumeTask = default;
		private double volume = default;
		private ControlService controlService;
		private PeriodicTimer timer;

		[ObservableProperty]
		private UsableAppDto selectedApp;

		public MainPageModel(ControlService controlService)
		{
			this.controlService = controlService;
			this.UsableApps = new ObservableCollection<UsableAppDto>();
			this.timer = new PeriodicTimer(TimeSpan.FromSeconds(50));
			this.RefreshApps(true);
		}




		private async Task RefreshApps(bool repeat = false)
		{
			var apps = await this.controlService.GetAppsAsync();
			var appsToAdd = apps.ExceptBy(UsableApps.Select(app=>app.Id),(app)=>app.Id);
			var appsToRemove = UsableApps.ExceptBy(apps.Select(app => app.Id), (app) => app.Id);


			foreach (var app in appsToRemove)
			{
				UsableApps.Remove(app);
			}
			foreach (var app in appsToAdd)
			{
				UsableApps.Add(app);
			}

			while (repeat)
			{
				await timer.WaitForNextTickAsync();
				await this.RefreshApps();
			}

		}

		[ObservableProperty]
		private bool canChangeVolume;

		public ObservableCollection<UsableAppDto> UsableApps { get; }



		[ICommand]
		private async void PlayPause()
		{
			await this.controlService.PressKeyAsync(KeyCode.MEDIA_PLAY_PAUSE, SelectedApp?.Id);
			
		}

		[ICommand]
		private async void SelectedAppChanged()
		{
			this.volumeTask = this.controlService.GetVolumeAsync(SelectedApp.Id).ContinueWith(task=>
			{
				this.volume = task.Result;
				this.CanChangeVolume = (task.Result != -1);
				return task.Result;
			});
		}

		[ICommand]
		private async void VolumeUp()
		{
			await this.volumeTask;
			this.volume += VolumeIncrement;
			
			await this.controlService.SetVolumeAsync(SelectedApp.Id, this.volume);
		}

		[ICommand]
		private async void VolumeDown()
		{
			await this.volumeTask;
			this.volume -= VolumeIncrement;
			await this.controlService.SetVolumeAsync(SelectedApp.Id, this.volume) ;
		}
	}
}
