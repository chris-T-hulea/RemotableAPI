using System;
using System.Collections.Generic;
using ServiceLayer.Interfaces;
using Utils;

namespace ServiceLayer
{
	public class LinkageService : ILinkageService
	{
		private readonly Constants _constants;
		private readonly TimeSpan refreshTime = TimeSpan.FromMinutes(1);
		private DateTimeOffset lastUpdate = DateTimeOffset.MinValue;
		private Guid currentId = Guid.Empty;
		private List<Guid> savedIds = new List<Guid>();

		public LinkageService(Constants constants)
		{
			_constants = constants;
		}

		public string GetGuid()
		{
			if (this._constants.DevMode)
			{
				return Encode(currentId);
			}

			DateTimeOffset timestamp = DateTimeOffset.Now;
			if (currentId == Guid.Empty
					|| timestamp - lastUpdate > refreshTime)
			{
				currentId = Guid.NewGuid();
			}

			return Encode(currentId);
		}

		public void SaveId(Guid guid)
		{
			if (!savedIds.Contains(guid))
			{
				savedIds.Add(guid);
			}
		}

		public bool CheckId(string guid)
		{
			var decoded = Decode(guid);
			return savedIds.Contains(decoded) || currentId == decoded;
		}

		private static string Encode(Guid guid)
		{
			string encoded = Convert.ToBase64String(guid.ToByteArray());
			encoded = encoded.Replace("/", "_").Replace("+", "-");
			return encoded.Substring(0, 22);
		}

		private static Guid Decode(string value)
		{
			value = value.Replace("_", "/").Replace("-", "+");
			byte[] buffer = Convert.FromBase64String(value + "==");
			return new Guid(buffer);
		}
	}
}