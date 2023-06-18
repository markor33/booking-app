using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ratings.API.Extensions;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;
using RatingsLibrary.Services;
using System.Data;


namespace Ratings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostRatingController : ControllerBase
    {
        private readonly IHostRatingService _hostRatingService;
        private readonly IReservationRepository _reservationRepository;

        public HostRatingController(IHostRatingService hostRatingService, IReservationRepository reservationRepository)
        {
            _hostRatingService = hostRatingService;
            _reservationRepository = reservationRepository;
        }

        [HttpGet("{hostId}")]
        public ActionResult<List<HostRating>> GetByHost(Guid hostId)
        {
            return Ok(_hostRatingService.GetAllByHost(hostId));
        }

        [HttpGet("average/{hostId}")]
        public ActionResult<List<HostRating>> GetAverageRatingByHost(Guid hostId)
        {
            return Ok(_hostRatingService.GetAverageByHost(hostId));
        }

        [Authorize(Roles = "GUEST")]
        [HttpPost("{resId}")]
        public ActionResult<int> RateHost(HostRating hostRating, Guid resId)
        {
            var res = _reservationRepository.GetById(resId);
            hostRating.HostId = res.HostId;
            hostRating.GuestId = Guid.Parse(User.UserId());
            return Ok(_hostRatingService.CreateOrEditHostRating(hostRating));
        }

        [Authorize(Roles = "GUEST")]
        [HttpDelete("{resId}")]
        public ActionResult DeleteHostRating(Guid resId)
        {
            _hostRatingService.DeleteHostRating(resId);
            return Ok();
        }

        [HttpGet("grades")]
        public ActionResult<List<int>> GetGradeByReservation()
        {
            return Ok(_hostRatingService.GetGradesByGuest(Guid.Parse(User.UserId()), User.UserRole()));
        }
    }
}
