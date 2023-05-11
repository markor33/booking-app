using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpDelete]
        public ActionResult CancelReservation(Guid reservationId)
        {
            _reservationService.CancelReservation(reservationId);
            return Ok();
        }
    }
}
