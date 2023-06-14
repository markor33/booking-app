using EventBus.NET.Integration;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.IntegrationEvents
{
    public class HostSearchAccommodationsDeletedIntegrationEventHandler : IIntegrationEventHandler<HostSearchAccommodationsDeletedIntegrationEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HostSearchAccommodationsDeletedIntegrationEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(HostSearchAccommodationsDeletedIntegrationEvent @event)
        {
            var user = await _userManager.FindByIdAsync(@event.HostId.ToString());
            user.Status = UserStatus.DELETED;
            await _userManager.UpdateAsync(user);
        }
    }
}
