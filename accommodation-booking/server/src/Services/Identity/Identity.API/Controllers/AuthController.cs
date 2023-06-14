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

        public AuthController(LoginService loginService, RegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
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
