using EventBus.NET.Integration;
using Notifications.SignalR.Models;
using Notifications.SignalR.Services;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class HostReviewedNotificationHandler : IIntegrationEventHandler<HostReviewedNotification>
    {
        private readonly INotificationService _notificationService;

        public HostReviewedNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleAsync(HostReviewedNotification @event)
        {
            var text = "You have got new review";
            var notification = new Notification(@event.HostId, text, typeof(HostReviewedNotification).Name);

            await _notificationService.SendNotification(notification);
        }
    }
}
