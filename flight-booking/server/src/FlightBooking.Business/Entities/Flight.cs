using FlightBooking.Business.Entities.Base;
using FlightBooking.Business.Helpers.CustomAttributes;

namespace FlightBooking.Business.Entities
{
    [BsonCollection("Flights")]
    public class Flight : BaseEntity
    {
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public int TicketPrice { get; set; }
        public int NumOfAvailableTickets { get; set; }
        public string? ImgUrl { get; set; }
    }
}
