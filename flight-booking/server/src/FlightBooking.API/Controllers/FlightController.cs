using CloudinaryDotNet.Actions;
using FlightBooking.API.Infrastructure;
using FlightBooking.Business.Entities;
using FlightBooking.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FlightBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IImageUploader _imageUploader;

        public FlightController(IFlightService flightService, IImageUploader imageUploader)
        {
            _flightService = flightService;
            _imageUploader = imageUploader;
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

        [HttpGet("search")]
        public async Task<ActionResult<List<Flight>>> Search(
            [FromQuery] DateTime date, 
            [FromQuery] string origin, 
            [FromQuery] string destination,
            [FromQuery] int numberOfPassengers)
        {
            var flights = await _flightService.SearchAsync(date, origin, destination, numberOfPassengers);
            return Ok(flights);
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> Create(Flight flight)
        {
            flight.ImgUrl = _imageUploader.UploadImage(flight.ImgUrl, flight.Destination);
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
