using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WindowsInput.Native;
using ServiceLayer.Interfaces;

namespace RestedApi.Controllers
{
	//[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class TestController : ControllerBase
	{
		private readonly IControlService _server;

		private readonly ILogger<TestController> _logger;

		// The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
		private static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

		public TestController(ILogger<TestController> logger, IControlService server)
		{
			_server = server;
			_logger = logger;
		}

		[HttpGet]
		public void Get(int keyId)
		{
			// HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

			_server.SendGlobalKey((VirtualKeyCode)keyId);
		}
	}
}