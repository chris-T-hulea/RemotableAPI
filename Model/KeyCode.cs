using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Model
{
	public enum KeyCode
	{
		None = -1,
		MEDIA_PLAY_PAUSE = 0,
		MEDIA_STOP = 1,
		MEDIA_NEXT_TRACK = 2,
		MEDIA_PREV_TRACK = 3,
		VOLUME_MUTE = 4,
		VOLUME_UP = 5,
		VOLUME_DOWN = 6,
	}

	public static class KeyCodeExtensions
	{
		public static VirtualKeyCode VirtualCode(this KeyCode code)
		{
			var succes = Enum.TryParse(code.ToString(), true, out VirtualKeyCode result);

			return succes ? result : default;
		}

		public static KeyCode GetCode(this VirtualKeyCode code)
		{
			var succes = Enum.TryParse(code.ToString(), true, out KeyCode result);
			return succes ? result : default;
		}
	}
}