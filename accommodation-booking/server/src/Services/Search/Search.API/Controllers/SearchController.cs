using Microsoft.AspNetCore.Mvc;
using Search.API.DTO;
using Search.API.Services;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<ActionResult<List<AccommodationDTO>>> Search(SearchArgs args)
        {
            var result = await _searchService.SearchAccommodations(args);
            return Ok(result);
        }
    }
}
