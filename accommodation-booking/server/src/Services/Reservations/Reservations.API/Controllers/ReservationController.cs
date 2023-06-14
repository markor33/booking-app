using AutoMapper;
using Identity.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.DTO;
using ReservationsLibrary.Models;
using ReservationsLibrary.Services;

namespace Reservations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(
            IReservationService reservationService,
            IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
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
        public ActionResult<List<ReservationDTO>> GetByUser()
        {
            var reservations = _reservationService.GetByUser(Guid.Parse(User.UserId()), User.UserRole());
            return Ok(_mapper.Map<List<ReservationDTO>>(reservations));
        }
    }
}
