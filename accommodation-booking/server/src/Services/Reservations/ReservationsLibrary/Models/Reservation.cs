using ReservationsLibrary.Utils;

namespace ReservationsLibrary.Models
{
    public class Reservation : BaseEntityModel
    {
        public Accommodation? Accommodation { get; set; }
        public Guid AccommodationId { get; set; }
        public Guid GuestId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public int Price { get; set; }
        public bool Canceled { get; set; } = false;

        public Reservation() { }

        public Reservation(Guid id, Guid guestId ,Guid accommodationId, DateRange period, int numOfGuests, int price)
        {
            Id = id;
            GuestId = guestId;
            AccommodationId = accommodationId;
            Period = period;
            NumOfGuests = numOfGuests;
            Price = price;
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
