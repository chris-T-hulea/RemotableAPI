using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class UsableAppDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }

		public string Icon { get; set; }

		public string Image => @"https://static-cdn.jtvnw.net/jtv_user_pictures/354925d4-8b77-4e04-90da-95ebd458d4a9-profile_image-70x70.png";
	}
}