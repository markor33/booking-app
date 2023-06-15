using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents.Notifications
{
    public class ReservationRequestCreatedNotification : IntegrationEvent
    {
        public Guid HostId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid ReservationRequestId { get; private set; }
        public bool IsAutoAccept { get; private set; }

        [JsonConstructor]
        public ReservationRequestCreatedNotification(Guid hostId, Guid accommodationId, Guid reservationRequestId, bool isAutoAccept)
        {
            HostId = hostId;
            AccommodationId = accommodationId;
            ReservationRequestId = reservationRequestId;
            IsAutoAccept = isAutoAccept;
        }

    }
}
