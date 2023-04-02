using FlightBooking.API.DTO;
using FlightBooking.API.Infrastructure;
using FlightBooking.Business.Entities;
using FlightBooking.Business.Services;
using HospitalAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FlightBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _userService.UpdateAsync(user);
            return Ok();
        }

    }
}
