using Web.Bff.Utils;

namespace Web.Bff.Models
{
    public class AggregateReservation
    {
        public string Id { get; set; }
        public string AccommodationId { get; set; }
        public string GuestId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public int Price { get; set; }
        public bool Canceled { get; set; } = false;
        public int HRating { get; set; } = 0;
        public int ARating { get; set; } = 0;
        public string UserFullName { get; set; }
        public string AccommPhoto { get; set; }
        public string AccommName { get; set; }
    }
}
