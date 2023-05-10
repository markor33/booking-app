using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace Reservations.API.DTO
{
    public class ReservationRequestDTO
    {
        public Guid Id { get; set; }
        public Guid AccommodationId { get; set; }
        public Guid? GuestId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public Price? Price { get; set; }
        public Guid PriceId { get; set; }
        public ReservationRequestStatus Status { get; set; } = ReservationRequestStatus.ON_HOLD;
    }
}
