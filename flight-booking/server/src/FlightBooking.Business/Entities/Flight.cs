using FlightBooking.Business.Entities.Base;
using FlightBooking.Business.Helpers.CustomAttributes;

namespace FlightBooking.Business.Entities
{
    [BsonCollection("flights")]
    public class Flight : BaseEntity
    {
        public string? From { get; set; }
        public string? To { get; set; }
    }
}
