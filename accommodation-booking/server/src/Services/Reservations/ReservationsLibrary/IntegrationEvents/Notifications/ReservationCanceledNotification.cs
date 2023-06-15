using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents.Notifications
{
    public class ReservationCanceledNotification : IntegrationEvent
    {
        public Guid HostId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid ReservationId { get; private set; }

        [JsonConstructor]
        public ReservationCanceledNotification(Guid hostId, Guid accommodationId, Guid reservationId)
        {
            HostId = hostId;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
        }
    }
}
