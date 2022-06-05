using System.Linq;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using Model.Entities;

namespace ServiceLayer
{
	public class VolumeService
	{
		private readonly CoreAudioDevice _device;

		public VolumeService()
		{
			this._device = new CoreAudioController().GetDefaultDevice(DeviceType.Playback, Role.Multimedia);
		}

		public void SetVolume(UsableApp app, double volume)
		{
			var session = this._device.SessionController.FirstOrDefault(ses => ses.ProcessId == app.Id);

			if (session == null)
			{
				return;
			}

			session.Volume = volume;
		}

		public double GetVolume(int id)
		{
			var session = this._device.SessionController.FirstOrDefault(ses => ses.ProcessId == id);

			if (session == null)
			{
				return -1;
			}

			return session.Volume;
		}

		public void SetVolume(double volume)
		{
			this._device.Volume = volume;
		}

		public double GetVolume()
		{
			return this._device.Volume;
		}
	}
}