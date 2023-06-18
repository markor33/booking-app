using Microsoft.AspNetCore.Mvc;
using Ratings.API.Extensions;
using RatingsLibrary.Models;
using RatingsLibrary.Services;

namespace Ratings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationRatingController : ControllerBase
    {
        private readonly IAccommodationRatingService _accommodationRatingService;
        
        public AccommodationRatingController(IAccommodationRatingService accommodationRatingService)
        {
            _accommodationRatingService = accommodationRatingService;
        }

        [HttpGet("{accommId}")]
        public ActionResult<List<HostRating>> GetByAccommodation(Guid accommId)
        {
            return Ok(_accommodationRatingService.GetAllByAccommodation(accommId));
        }

        [HttpGet("average/{accommId}")]
        public ActionResult<List<HostRating>> GetAverageRatingByAccommodation(Guid accommId)
        {
            return Ok(_accommodationRatingService.GetAverageByAccommodation(accommId));
        }

        [HttpPost]
        public ActionResult<int> RateAccommodation(AccommodationRating accommodationRating)
        {
            accommodationRating.GuestId = Guid.Parse(User.UserId());
            return Ok(_accommodationRatingService.CreateOrEditAccommodationRating(accommodationRating));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAccommodationRating(Guid id)
        {
            _accommodationRatingService.DeleteAccommodationRating(Guid.Parse(User.UserId()), id);
            return Ok();
        }
    }
}
