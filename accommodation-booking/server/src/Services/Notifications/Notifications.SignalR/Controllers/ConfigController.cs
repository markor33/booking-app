using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.SignalR.Extensions;
using Notifications.SignalR.Models;
using Notifications.SignalR.Persistence.Repositories;

namespace Notifications.SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigController : ControllerBase
    {
        private readonly IUserNotificationsConfigRepository _configRepository;

        public ConfigController(IUserNotificationsConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, bool>>> Get()
        {
            var userId = Guid.Parse(User.UserId());
            var config = await _configRepository.GetByUser(userId);

            if (config is null)
                return NotFound();

            return Ok(config.NotificationsConfig);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Dictionary<string, bool> notificationsConfig)
        {
            var userId = Guid.Parse(User.UserId());

            var userConfig = await _configRepository.GetByUser(userId);
            if (userConfig is null)
                await _configRepository.Create(new UserNotificationsConfig(userId, notificationsConfig));
            else
            {
                userConfig.NotificationsConfig = notificationsConfig;
                await _configRepository.Update(userConfig);
            }

            return Ok();
        }

    }
}
