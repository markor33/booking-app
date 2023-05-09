using Identity.API.Extensions;
using Identity.API.Models;
using Identity.API.Services;
using Identity.API.Services.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserService _userService;

        public ApplicationUserController(ApplicationUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPut("edit/profile")]
        public async Task<ActionResult> EditProfile(RegisterViewModel user)
        {
            user.Email = User.UserEmail();
            var res = await _userService.EditProfileAsync(user);
            if (res.IsFailed)
                return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpPut("change/credentials")]
        public async Task<ActionResult> ChangeCredentials(Credentials credentials)
        {
            credentials.Email = User.UserEmail();
            var res = await _userService.ChangeCredentials(credentials);
            if (res.IsFailed)
                return BadRequest();
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser()
        {
            var res = await _userService.DeleteUser(User.UserId(), User.UserRole());
            if (res.IsFailed)
                return BadRequest();
            return Ok();
        }
    }
}
