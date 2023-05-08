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

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "Host")]
        [HttpGet("canceled/reservations")]
        public ActionResult<int> NumberOfCancelByGuest(Guid guestId)
        {
            return Ok(_reservationService.NumOfCanceledReservationForGuest(guestId));
        }
        [Authorize(Roles = "Guest")]
        [HttpDelete]
        public ActionResult CancelReservation(Guid reservationId)
        {
            _reservationService.CancelReservation(reservationId);
            return Ok();
        }
    }
}
