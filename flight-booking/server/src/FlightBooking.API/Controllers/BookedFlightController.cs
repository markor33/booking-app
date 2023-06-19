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
    public class BookedFlightController : ControllerBase
    {
        private readonly IBookedFlightService _bookedFlightService;

        public BookedFlightController(IBookedFlightService bookedFlightService)
        {
            _bookedFlightService = bookedFlightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookedFlight>>> GetAll()
        {
            return Ok(await _bookedFlightService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _bookedFlightService.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BookedFlight>> Create(BookedFlightDTO flightDTO)
        {
            var bookedFlight = new BookedFlight() {
                UserId = User.UserId(),
                FlightId = flightDTO.FlightId,
                NumberOfTickets = flightDTO.NumberOfTickets
            };
            var flight = await _bookedFlightService.CreateAsync(bookedFlight);
            if(flight == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = flight.Id }, flight);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.UserId();
            return Ok(await _bookedFlightService.GetByUserIdAsync(userId));
        }

    }
}
