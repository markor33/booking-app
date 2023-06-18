using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RatingsLibrary.IntegrationEvents
{
    public class AccommodationRatingDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid ReviewId { get; private set; }
        public Guid GuestId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public int Rating { get; private set; }
        public DateTime Date { get; private set; }

        [JsonConstructor]
        public AccommodationRatingDeletedIntegrationEvent(Guid reviewId, Guid guestId, Guid accommodationId, int rating, DateTime date)
        {
            ReviewId = reviewId;
            GuestId = guestId;
            AccommodationId = accommodationId;
            Rating = rating;
            Date = date;
        }
    }
}
