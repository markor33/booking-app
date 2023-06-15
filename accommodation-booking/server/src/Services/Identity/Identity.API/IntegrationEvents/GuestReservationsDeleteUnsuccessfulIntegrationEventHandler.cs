using EventBus.NET.Integration;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.IntegrationEvents
{
    public class GuestReservationsDeleteUnsuccessfulIntegrationEventHandler : IIntegrationEventHandler<GuestReservationsDeleteUnsuccessfulIntegrationEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GuestReservationsDeleteUnsuccessfulIntegrationEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(GuestReservationsDeleteUnsuccessfulIntegrationEvent @event)
        {
            var user = await _userManager.FindByIdAsync(@event.GuestId.ToString());
            user.Status = UserStatus.ACTIVE;
            await _userManager.UpdateAsync(user);
        }
    }
}
