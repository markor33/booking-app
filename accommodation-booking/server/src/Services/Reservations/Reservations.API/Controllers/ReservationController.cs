using Identity.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationsLibrary.Models;
using ReservationsLibrary.Services;

namespace Reservations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(
            IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "HOST")]
        [HttpGet("canceled/reservations/{id}")]
        public ActionResult<int> NumberOfCancelByGuest([FromRoute] Guid id)
        {
            return Ok(_reservationService.NumOfCanceledReservationForGuest(id));
        }

        [Authorize(Roles = "GUEST")]
        [HttpPut("{reservationId}")]
        public ActionResult CancelReservation([FromRoute] Guid reservationId)
        {
            var res = _reservationService.CancelReservation(reservationId);
            if (res.IsFailed)
                return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<List<Reservation>> GetByUser()
        {
            return _reservationService.GetByUser(Guid.Parse(User.UserId()), User.UserRole());
        }
    }
}
