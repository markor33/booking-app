using Ratings.Models;
using Ratings.Utils;

namespace RatingsLibrary.Models
{
    public class Reservation : BaseEntityModel
    {
        public Guid GuestId { get; set; }
        public Guid AccommodationId { get; set; }
        public Guid HostId { get; set; }
        public DateRange Period { get; set; }
        public bool Canceled { get; set; }
    }
}
