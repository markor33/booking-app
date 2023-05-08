using Microsoft.AspNetCore.Mvc;
using Reservations.API.DTO;
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

        [HttpGet("canceled/reservations")]
        public ActionResult<int> NumberOfCancelByGuest(Guid guestId)
        {
            return Ok(_reservationService.NumOfCanceledReservationForGuest(guestId));
        }
        [HttpDelete]
        public ActionResult CancelReservation(Guid reservationId)
        {
            _reservationService.CancelReservation(reservationId);
            return Ok();
        }
    }
}
