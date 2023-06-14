using ReservationsLibrary.Utils;

namespace Reservations.API.DTO
{
    public class ReservationDTO
    {
        public Guid Id { get; set; }
        public Guid AccommodationId { get; set; }
        public Guid GuestId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public int Price { get; set; }
        public bool Canceled { get; set; } = false;
    }
}
