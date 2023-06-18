using Microsoft.AspNetCore.Mvc;
using Ratings.API.Extensions;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;
using RatingsLibrary.Services;


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

        [HttpPost("{resId}")]
        public ActionResult<int> RateHost(HostRating hostRating, Guid resId)
        {
            var res = _reservationRepository.GetById(resId);
            hostRating.HostId = res.HostId;
            hostRating.GuestId = Guid.Parse(User.UserId());
            return Ok(_hostRatingService.CreateOrEditHostRating(hostRating));
        }

        [HttpDelete("{resId}")]
        public ActionResult DeleteHostRating(Guid resId)
        {
            var res = _reservationRepository.GetById(resId);
            _hostRatingService.DeleteHostRating(Guid.Parse(User.UserId()), res.HostId);
            return Ok();
        }
        [HttpGet("grades")]
        public ActionResult<List<int>> GetAllGradesByGuest()
        {
            return Ok(_hostRatingService.GetAllGradesByGuest(Guid.Parse(User.UserId())));
        }
    }
}
