namespace RecommendationSystem.API.Models
{
    public class Reservation
    {
        public Guid Id { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid GuestId { get; private set; }

        public Reservation(Guid id, Guid accommodationId, Guid guestId)
        {
            Id = id;
            AccommodationId = accommodationId;
            GuestId = guestId;
        }

    }
}
