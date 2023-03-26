using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;

        public FlightController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetAll()
        {
            return Ok(await _flightRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _flightRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> Create(Flight flight)
        {
            flight = await _flightRepository.CreateAsync(flight);
            return CreatedAtAction(nameof(Get), new { id = flight.Id }, flight);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Flight flight)
        {
            await _flightRepository.UpdateAsync(flight);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _flightRepository.DeleteAsync(id);
            return Ok();
        }

    }
}
