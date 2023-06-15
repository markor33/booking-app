using EventBus.NET.Integration;
using Notifications.SignalR.Models;
using Notifications.SignalR.Services;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class ReservationRequestStatusChangedNotificationHandler : IIntegrationEventHandler<ReservationRequestStatusChangedNotification>
    {
        private readonly INotificationService _notificationService;

        public ReservationRequestStatusChangedNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleAsync(ReservationRequestStatusChangedNotification @event)
        {
            var text = $"Reservation request has been changed. Status: {@event.IsConfirmed}";
            var notification = new Notification(@event.GuestId, text, typeof(ReservationRequestStatusChangedNotification).Name);

            await _notificationService.SendNotification(notification);
        }
    }
}
