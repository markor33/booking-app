using FlightBooking.Business.Entities.Base;
using FlightBooking.Business.Helpers.CustomAttributes;

namespace FlightBooking.Business.Entities
{
    [BsonCollection("Users")]
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

    }
}
