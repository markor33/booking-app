using EventBus.NET.Integration;
using Notifications.SignalR.Models;
using Notifications.SignalR.Services;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class ReservationRequestCreatedNotificationHandler : IIntegrationEventHandler<ReservationRequestCreatedNotification>
    {
        private readonly INotificationService _notificationService;

        public ReservationRequestCreatedNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleAsync(ReservationRequestCreatedNotification @event)
        {
            var text = "New reservation request has been created";
            var notification = new Notification(@event.HostId, text, typeof(ReservationRequestCreatedNotification).Name);

            await _notificationService.SendNotification(notification);
        }
    }
}
