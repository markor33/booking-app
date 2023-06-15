using EventBus.NET.Integration;
using Notifications.SignalR.Models;
using Notifications.SignalR.Services;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class ReservationCanceledNotificationHandler : IIntegrationEventHandler<ReservationCanceledNotification>
    {
        private readonly INotificationService _notificationService;

        public ReservationCanceledNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleAsync(ReservationCanceledNotification @event)
        {
            var text = "Reservation has been canceled";
            var notification = new Notification(@event.HostId, text, typeof(ReservationCanceledNotification).Name);

            await _notificationService.SendNotification(notification);
        }
    }
}
