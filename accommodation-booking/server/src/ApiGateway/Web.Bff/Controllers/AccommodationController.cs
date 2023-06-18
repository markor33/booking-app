using Microsoft.AspNetCore.Mvc;
using Search.API.DTO;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly AggregationService aggregationService;

        public AccommodationController(AggregationService aggregationService)
        {
            this.aggregationService = aggregationService;
        }

        [HttpPost("search")]
        public ActionResult<List<SearchExtended>> Search(SearchArgs args)
        {
            var result = aggregationService.SearchAccommodations(args);
            return Ok(result);
        }
    }
}
