using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class ReservationCanceledIntegrationEvent : IntegrationEvent
    {
        public Guid GuestId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid ReservationId { get; private set; }

        [JsonConstructor]
        public ReservationCanceledIntegrationEvent(Guid guestId, Guid accommodationId, Guid reservationId)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
        }

    }
}
