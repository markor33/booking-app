using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Bff.Extensions;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly AccommodationService _accommodationService;
        public AccommodationController(AccommodationService aggregationService)

        {
            _accommodationService = aggregationService;
        }

        [HttpPost("search")]
        public ActionResult<List<SearchExtended>> Search(SearchArgs args)
        {
            var result = _accommodationService.SearchAccommodations(args);
            return Ok(result);
        }

        [HttpGet("{accommId}/{hostId}")]
        public ActionResult<AccomodationDTO> GetAccommodationDialog(string accommId, string hostId)
        {
            var result = _accommodationService.GetAccommodationDialog(accommId, hostId);
            return Ok(result);
        }
    }
}
