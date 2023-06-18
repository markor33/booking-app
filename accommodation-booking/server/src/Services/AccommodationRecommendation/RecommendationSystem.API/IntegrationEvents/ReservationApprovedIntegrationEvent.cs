using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class ReservationApprovedIntegrationEvent : IntegrationEvent
    {
        public Guid ReservationId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid GuestId { get; private set; }

        [JsonConstructor]
        public ReservationApprovedIntegrationEvent(Guid reservationId, Guid accommodationId, Guid guestId)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            GuestId = guestId;
        }
    }
}
