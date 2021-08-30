using WindowsInput.Native;
using Model.DTO;

namespace Model.Entities
{
	public class KeyPressCommand
	{
		public VirtualKeyCode KeyCode { get; set; }

		public int TargetAppId { get; set; }
	}
}