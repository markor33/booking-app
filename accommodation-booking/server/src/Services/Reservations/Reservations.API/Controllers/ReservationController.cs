using Identity.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.Integration.EventBus;
using Reservations.API.Integration.Events;
using ReservationsLibrary.Models;
using ReservationsLibrary.Services;

namespace Reservations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IEventBus _eventBus;

        public ReservationController(
            IReservationService reservationService,
            IEventBus eventBus)
        {
            _reservationService = reservationService;
            _eventBus = eventBus;
        }

        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            _eventBus.Publish(new TestIntegrationEvent("asdasdasdasd"));
            return Ok();
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
