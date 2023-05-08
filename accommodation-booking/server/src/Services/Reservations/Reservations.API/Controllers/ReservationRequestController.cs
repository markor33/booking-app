using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reservations.API.DTO;
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

        [HttpGet("host")]
        public ActionResult<List<ReservationRequest>> GetByHost(Guid hostId)
        {
            return _reservationRequestService.GetByHost(hostId);
        }

        [HttpPut("approve")]
        public ActionResult ApproveRequest(Guid requestId)
        {
            _reservationRequestService.ApproveRequest(requestId);
            return Ok();
        }
        [HttpPut("decline")]
        public ActionResult DeclineRequest(Guid requestId)
        {
            _reservationRequestService.DeclineRequest(requestId);
            return Ok();
        }
        [HttpPost]
        public ActionResult CreateRequest(ReservationRequestDTO request)
        {
            _reservationRequestService.Create(_mapper.Map<ReservationRequest>(request));
            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteRequest(Guid requestId)
        {
            _reservationRequestService.DeleteRequest(requestId);
            return Ok();
        }
    }
}
