using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Interfaces;
using Model;

namespace ServiceLayer
{
	public class ServiceLayerBootstrapper
	{

		public void Run(IServiceCollection container)
		{
			container.AddScoped<ILinkageService, LinkageService>();
			container.AddScoped<VolumeService>();
			container.AddScoped<IControlService, ControlService>();
		}
	}
}