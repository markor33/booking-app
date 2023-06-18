using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RatingsLibrary.IntegrationEvents
{
    public class ReservationCanceledIntegrationEvent : IntegrationEvent
    {
        public Guid AccommodationId { get; private set; }
        public Guid ReservationId { get; private set; }

        [JsonConstructor]
        public ReservationCanceledIntegrationEvent(Guid accommodationId, Guid reservationId)
        {
            AccommodationId = accommodationId;
            ReservationId = reservationId;
        }

    }
}
