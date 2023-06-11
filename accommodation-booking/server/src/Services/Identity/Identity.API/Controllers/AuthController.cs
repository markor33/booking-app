using Identity.API.Integration;
using Identity.API.Integration.EventBus;
using Identity.API.Integration.Events;
using Identity.API.Services.Login;
using Identity.API.Services.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly RegisterService _registerService;
        private readonly IEventBus _eventBus;

        public AuthController(LoginService loginService, RegisterService registerService, IEventBus eventBus)
        {
            _loginService = loginService;
            _registerService = registerService;
            _eventBus = eventBus;
        }

        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            _eventBus.Publish(new TestIntegrationEvent("asdadasdasdasd"));
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginViewModel login)
        {
            var result = await _loginService.LoginAsync(login);
            if (result.IsFailed)
                return BadRequest();
            return Ok(result.Value);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            var result = await _registerService.RegisterAsync(register);
            if (result.IsFailed)
                return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpGet("validate")]
        public IActionResult Validate()
        {
            return Ok();
        }

    }
}
