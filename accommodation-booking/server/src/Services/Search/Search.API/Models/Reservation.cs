using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class Reservation
    {
        public Guid Id { get; private set; }
        public Guid GuestId { get; private set; }
        public DateRange Period { get; private set; }
        public bool IsDeleted { get; private set; }

        [JsonConstructor]
        public Reservation(Guid id, Guid guestId, DateRange period)
        {
            Id = id;
            GuestId = guestId;
            Period = period;
            IsDeleted = false;
        }
    }
}
