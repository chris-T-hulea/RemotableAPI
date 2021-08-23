using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace ServiceLayer.Interfaces
{
	public interface IControlService
	{
		void SendGlobalKey(VirtualKeyCode keyCode);
	}
}