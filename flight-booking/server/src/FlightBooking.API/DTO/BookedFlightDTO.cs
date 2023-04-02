using FlightBooking.Business.Entities;

namespace FlightBooking.API.DTO
{
    public class BookedFlightDTO
    {
        public string FlightId { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
