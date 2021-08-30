using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using RestedApi.Mapper;
using Utils;

namespace RestedApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

			services.Configure<Settings>(Configuration.GetSection(Settings.Name));

			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new ApplicationProfile());
			});
			services.AddSingleton(mappingConfig.CreateMapper());

			// Run bootstrapper
			new MainBootstrapper(services).Run();

			services.AddCors(options => options.AddPolicy("Debug", cors => cors.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
			services.AddCors(options => options.AddPolicy("localhost", cors => cors.WithOrigins("localHost:8080").AllowAnyHeader().AllowAnyMethod()));
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestedApi", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestedApi v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors("Debug");

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}