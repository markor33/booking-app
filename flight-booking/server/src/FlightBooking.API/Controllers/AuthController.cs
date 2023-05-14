using FlightBooking.API.Identity;
using FlightBooking.API.Identity.HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var result = await _authService.Login(loginRequest);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _authService.Register(registerRequest);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

    }
}
