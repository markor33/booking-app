    using FlightBooking.Business.Entities;
using FlightBooking.Business.Services;
using HospitalAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiKey>> Get()
        {
            var userId = User.UserId();
            var apiKey = await _apiKeyService.GetByUser(userId);

            if (apiKey is null)
                return NotFound();

            return Ok(apiKey);
        }

        [HttpPost]
        public async Task<ActionResult<ApiKey>> Create([FromQuery] bool isPermanent)
        {
            var userId = User.UserId();
            var apiKey = await _apiKeyService.Create(userId, isPermanent);

            if (apiKey is null)
                return BadRequest();

            return Ok(apiKey);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiKey>> Update(string id, [FromQuery] bool isPermanent)
        {
            var userId = User.UserId();
            var apiKey = await _apiKeyService.Update(id, isPermanent);

            return Ok(apiKey);
        }

        [HttpPut("{id}/refresh")]
        public async Task<ActionResult> RefreshToken(string id)
        {
            var apiKey = await _apiKeyService.RefreshExpireDate(id);

            return Ok(apiKey);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _apiKeyService.Delete(id);

            return Ok();
        }

    }
}
