using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface ILinkageService
	{
		string GetGuid();

		void SaveId(Guid guid);

		bool CheckId(string guid);
	}
}