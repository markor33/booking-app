using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RatingsLibrary.Models;
using RatingsLibrary.Services;
using System.Data;

namespace Ratings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostRatingController : ControllerBase
    {
        private readonly IHostRatingService _hostRatingService;

        public HostRatingController(IHostRatingService hostRatingService)
        {
            _hostRatingService = hostRatingService;
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

        [HttpPost]
        public ActionResult<int> RateHost(HostRating hostRating)
        {
            return Ok(_hostRatingService.CreateOrEditHostRating(hostRating));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHostRating(Guid id)
        {
            _hostRatingService.DeleteHostRating(id);
            return Ok();
        }
    }
}
