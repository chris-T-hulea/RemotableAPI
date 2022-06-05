using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using Model.Entities;

namespace ServiceLayer.Interfaces
{
	public interface IControlService
	{
		void SendGlobalKey(KeyPressCommand command);

		IEnumerable<UsableApp> GetApps();

		UsableApp FindApp(int pid);
	}
}