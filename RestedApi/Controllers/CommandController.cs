using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WindowsInput.Native;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Web.Resource;
using Model.DTO;
using Model.Entities;
using ServiceLayer;
using ServiceLayer.Interfaces;

namespace RestedApi.Controllers
{
	//[Authorize]
	[ApiController]
	[EnableCors("Debug")]
	[Route("[controller]")]
	public class CommandController : ControllerBase
	{
		private readonly IControlService _service;
		private readonly VolumeService _volumeService;

		private readonly ILogger<CommandController> _logger;
		private readonly IMapper _mapper;

		// The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
		private static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

		public CommandController(ILogger<CommandController> logger, IMapper mapper, IControlService service, VolumeService volumeService)
		{
			_service = service;
			_volumeService = volumeService;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult GetAll()
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			var apps = _service.GetApps().Select(app => this._mapper.Map<ApplicationDto>(app));

			return Ok(apps);
		}

		[HttpGet("Volume/{id}")]
		public ActionResult GetVolume(int id)
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			double volume = this._volumeService.GetVolume(id);

			return Ok(volume);
		}

		[HttpPost("Volume/{id}")]
		public ActionResult SetVolume(int id, double volume)
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			App app = this._service.FindApp(id);

			if (app == null)
			{
				this.BadRequest("Invalid id.");
			}

			this._volumeService.SetVolume(app, volume);

			return Ok();
		}

		[HttpGet("Volume")]
		public ActionResult GetVolume()
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			double volume = this._volumeService.GetVolume();

			return Ok(volume);
		}

		[HttpPost("Volume")]
		public ActionResult SetVolume(double volume)
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			this._volumeService.SetVolume(volume);

			return Ok();
		}

		[HttpPost("Talk/{message}")]
		public ActionResult Talk(string message)
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			Console.WriteLine(message);

			return Ok(message);
		}

		[HttpPost("KeyPress")]
		public StatusCodeResult Post(KeyPressDto command)
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			_service.SendGlobalKey(this._mapper.Map<KeyPressCommand>(command));

			return Ok();
		}
	}
}