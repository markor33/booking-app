using AutoMapper;
using Identity.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.DTO;
using Reservations.API.Infrasructure;
using ReservationsLibrary.Models;
using ReservationsLibrary.Services;

namespace Reservations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationRequestController : ControllerBase
    {
        private readonly IReservationRequestService _reservationRequestService;
        private readonly IMapper _mapper;

        public ReservationRequestController(IReservationRequestService reservationRequestService, IMapper mapper)
        {
            _reservationRequestService = reservationRequestService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<List<ReservationRequest>> GetByUser()
        {
            return _reservationRequestService.GetByUser(Guid.Parse(User.UserId()), User.UserRole());
        }

        [Authorize(Roles = "HOST")]
        [HttpPut("approve/{id}")]
        public ActionResult ApproveRequest([FromRoute] Guid id)
        {
            _reservationRequestService.ApproveRequest(id);
            return Ok();
        }
        [Authorize(Roles = "HOST")]
        [HttpPut("decline/{id}")]
        public ActionResult DeclineRequest([FromRoute] Guid id)
        {
            _reservationRequestService.DeclineRequest(id);
            return Ok();
        }
        //[Authorize(Roles = "GUEST")]
        [HttpPost]
        public ActionResult CreateRequest(ReservationRequestDTO request)
        {
            _reservationRequestService.Create(_mapper.Map<ReservationRequest>(request));
            return Ok();
        }
        [Authorize(Roles = "GUEST")]
        [HttpDelete("{id}")]
        public ActionResult DeleteRequest([FromRoute] Guid id)
        {
            _reservationRequestService.DeleteRequest(id);
            return Ok();
        }
    }
}
