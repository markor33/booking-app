using ReservationsLibrary.Utils;

namespace ReservationsLibrary.Models
{
    public class Reservation : BaseEntityModel
    {
        public Accommodation? Accommodation { get; set; }
        public Guid AccommodationId { get; set; }
        public Guid? GuestId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public Price? Price { get; set; }
        public Guid PriceId { get; set; }
        public bool Canceled { get; set; } = false;

        public Reservation() { }

        public Reservation(Guid id, Guid guestId ,Guid accommodationId, DateRange period, int numOfGuests, Guid priceId)
        {
            Id = id;
            GuestId = guestId;
            AccommodationId = accommodationId;
            Period = period;
            NumOfGuests = numOfGuests;
            PriceId = priceId;
        }

        public Reservation(ReservationRequest request)
        {
            Accommodation = request.Accommodation;
            GuestId = request.GuestId;
            Period = request.Period;
            NumOfGuests = request.NumOfGuests;
            Price = request.Price;
            Canceled = false;
        }

    }
}
