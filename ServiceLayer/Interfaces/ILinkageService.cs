using System;

namespace ServiceLayer.Interfaces
{
	public interface ILinkageService
	{
		string GetGuid();

		void SaveId(Guid guid);

		bool CheckId(string guid);
	}
}