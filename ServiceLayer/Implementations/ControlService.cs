using WindowsInput;
using WindowsInput.Native;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
	internal class ControlService : IControlService
	{
		private InputSimulator sim = new InputSimulator();

		public void SendGlobalKey(VirtualKeyCode keyCode)
		{
			sim.Keyboard.KeyPress(new[] { keyCode });
		}
	}
}