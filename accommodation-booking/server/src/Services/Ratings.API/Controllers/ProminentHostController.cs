using Microsoft.AspNetCore.Mvc;
using Ratings.API.DTO;
using RatingsLibrary.Models;
using RatingsLibrary.Services;

namespace Ratings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProminentHostController : ControllerBase
    {
        private readonly IProminentHostService _prominentHostService;
        private readonly IHostRatingService _hostRatingService;
        private readonly IAccommodationRatingService _accommodationRatingService;

        public ProminentHostController(IProminentHostService prominentHostService, IHostRatingService hostRatingService, IAccommodationRatingService accommodationRatingService)
        {
            _prominentHostService = prominentHostService;
            _hostRatingService = hostRatingService;
            _accommodationRatingService = accommodationRatingService;
        }

        [HttpGet("{hostId}")]
        public ActionResult<bool> IsHostProminent(Guid hostId)
        {
            return Ok(_prominentHostService.IsHostProminent(hostId));
        }
    }
}
