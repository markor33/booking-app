using EventBus.NET.Integration;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.IntegrationEvents
{
    public class GuestReservationsDeletedIntegrationEventHandler : IIntegrationEventHandler<GuestReservationsDeletedIntegrationEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GuestReservationsDeletedIntegrationEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(GuestReservationsDeletedIntegrationEvent @event)
        {
            var user = await _userManager.FindByIdAsync(@event.GuestId.ToString());
            user.Status = UserStatus.DELETED;
            await _userManager.UpdateAsync(user);
        }
    }
}
