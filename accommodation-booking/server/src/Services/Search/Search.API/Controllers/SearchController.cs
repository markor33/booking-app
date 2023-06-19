using Microsoft.AspNetCore.Mvc;
using Search.API.DTO;
using Search.API.Persistence.Repositories;
using Search.API.Services;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IAccommodationRepository _accommodationRepository;

        public SearchController(ISearchService searchService, IAccommodationRepository accommodationRepository)
        {
            _searchService = searchService;
            _accommodationRepository = accommodationRepository;
        }

        [HttpPost]
        public async Task<ActionResult<List<AccommodationDTO>>> Search(SearchArgs args)
        {
            var result = await _searchService.SearchAccommodations(args);
            return Ok(result);
        }

        [HttpPost("availability")]
        public async Task<ActionResult<AccommodationDTO>> CheckAvailability(CheckAvailabilityArgs args)
        {
            var result = await _accommodationRepository.CheckAvailability(args);
            if (result is null)
                return BadRequest();
            return Ok(new AccommodationDTO(result, args.NumOfGuests, args.Start, args.End));
        }

    }
}
