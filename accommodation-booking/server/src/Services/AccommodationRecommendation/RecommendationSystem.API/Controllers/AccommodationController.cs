using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecommendationSystem.API.DTO;
using RecommendationSystem.API.Extensions;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationController(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        [HttpGet]
        [Authorize(Roles = "GUEST")]
        public async Task<ActionResult<List<RecommendedAccommodationDTO>>> GetRecommended()
        {
            var userId = Guid.Parse(User.UserId());

            return await _accommodationRepository.GetRecommended(userId);
        }

    }
}
