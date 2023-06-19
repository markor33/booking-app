namespace RecommendationSystem.API.Models
{
    public class Review
    {
        public Guid Id { get; private set; }
        public Guid GuestId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public int Rating { get; private set; }
        public DateOnly Date { get; private set; }

        public Review(Guid id, Guid guestId, Guid accommodationId, int rating, DateOnly date)
        {
            Id = id;
            GuestId = guestId;
            AccommodationId = accommodationId;
            Rating = rating;
            Date = date;
        }
    }
}
