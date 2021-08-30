using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using AutoMapper;
using Model;
using Model.DTO;
using Model.Entities;

namespace RestedApi.Mapper
{
	public class ApplicationProfile : Profile
	{
		public ApplicationProfile()
		{
			_ = CreateMap<App, ApplicationDto>()
				.ForMember(
					dest => dest.Icon,
					opt =>
						opt.MapFrom(
							src => GetStringIcon(src))).
				ReverseMap()
				.ForMember(
					dest => dest.Process,
				opt => opt.MapFrom(src => Process.GetProcessById(src.Id)));

			CreateMap<KeyPressCommand, KeyPressDto>()
				.ForMember(
					dest => dest.KeyCode,
					opt => opt.MapFrom(src => src.KeyCode.GetCode()))
				.ReverseMap()
				.ForMember(
					dest => dest.KeyCode,
					opt => opt.MapFrom(src => src.KeyCode.VirtualCode()));
		}

		private static string GetStringIcon(App src)
		{
			Icon icon = Icon.ExtractAssociatedIcon(src.Process.MainModule.FileName);
			byte[] bytes;
			using (var ms = new MemoryStream())
			{
				icon.Save(ms);
				return Convert.ToBase64String(ms.ToArray());
			}
		}
	}
}