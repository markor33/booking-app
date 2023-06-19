using EventBus.NET.Integration;
using Notifications.SignalR.Models;
using Notifications.SignalR.Services;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class HostProminentStatusChangedHandler : IIntegrationEventHandler<HostProminentStatusChanged>
    {
        private readonly INotificationService _notificationService;

        public HostProminentStatusChangedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleAsync(HostProminentStatusChanged @event)
        {
            var text = "You'r prominent status has changed " + @event.IsProminent;
            var notification = new Notification(@event.HostId, text, typeof(HostProminentStatusChanged).Name);

            await _notificationService.SendNotification(notification);
        }
    }
}
