using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Bff.Extensions;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<List<AggregateReservation>> GetReservationByUser()
        {
            var result = _reservationService.GetReservationsByUser(User.UserId(), User.UserRole().ToString());
            return Ok(result);
        }
    }
}
