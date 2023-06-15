using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class ReservationRequestStatusChangedNotification : IntegrationEvent
    {
        public Guid GuestId { get; private set; }
        public bool IsConfirmed { get; private set; }

        [JsonConstructor]
        public ReservationRequestStatusChangedNotification(Guid guestId, bool isConfirmed)
        {
            GuestId = guestId;
            IsConfirmed = isConfirmed;
        }
    }
}
