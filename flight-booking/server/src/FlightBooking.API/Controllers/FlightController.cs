using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FlightBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetAll()
        {
            return Ok(await _flightService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _flightService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> Create(Flight flight)
        {
            flight = await _flightService.CreateAsync(flight);
            return CreatedAtAction(nameof(Get), new { id = flight.Id }, flight);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Flight flight)
        {
            await _flightService.UpdateAsync(flight);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            await _flightService.DeleteAsync(id);
            return Ok();
        }

    }
}
