using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Model.DTO
{
	public class KeyPressDto
	{
		public KeyCode KeyCode { get; set; }

		public int TargetAppId { get; set; }
	}
}