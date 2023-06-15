using Microsoft.AspNetCore.Mvc;
using RatingsLibrary.Models;
using RatingsLibrary.Services;

namespace Ratings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProminentHostController : ControllerBase
    {
        private readonly IProminentHostService _prominentHostService;

        public ProminentHostController(IProminentHostService prominentHostService)
        {
            _prominentHostService = prominentHostService;
        }

        [HttpGet("{hostId}")]
        public ActionResult<bool> IsHostProminent(Guid hostId)
        {
            return Ok(_prominentHostService.IsHostProminent(hostId));
        }
    }
}
