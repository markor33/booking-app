using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ratings.API.Extensions;
using RatingsLibrary.Models;
using RatingsLibrary.Services;
using System.Data;

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

        [Authorize(Roles = "GUEST")]
        [HttpPost]
        public ActionResult<int> RateAccommodation(AccommodationRating accommodationRating)
        {
            accommodationRating.GuestId = Guid.Parse(User.UserId());
            return Ok(_accommodationRatingService.CreateOrEditAccommodationRating(accommodationRating));
        }

        [Authorize(Roles = "GUEST")]
        [HttpDelete("{id}")]
        public ActionResult DeleteAccommodationRating(Guid id)
        {
            _accommodationRatingService.DeleteAccommodationRating(id);
            return Ok();
        }

        [HttpGet("grades")]
        public ActionResult<List<int>> GetGradeByReservation()
        {
            return Ok(_accommodationRatingService.GetGradesByGuest(Guid.Parse(User.UserId()), User.UserRole()));
        }
    }
}
