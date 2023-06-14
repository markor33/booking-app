using EventBus.NET.Integration;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.IntegrationEvents
{
    public class HostReservationsDeleteUnsuccessfulIntegrationEventHandler : IIntegrationEventHandler<HostReservationsDeleteUnsuccessfulIntegrationEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HostReservationsDeleteUnsuccessfulIntegrationEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(HostReservationsDeleteUnsuccessfulIntegrationEvent @event)
        {
            var user = await _userManager.FindByIdAsync(@event.HostId.ToString());
            user.Status = UserStatus.ACTIVE;
            await _userManager.UpdateAsync(user);
        }
    }
}
