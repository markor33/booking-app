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
        private readonly ReservationService _reservationService;
        public AccommodationController(AccommodationService aggregationService, ReservationService reservationService)

        {
            _accommodationService = aggregationService;
            _reservationService = reservationService;
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
