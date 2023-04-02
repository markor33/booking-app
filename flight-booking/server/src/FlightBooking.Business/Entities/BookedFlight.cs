using FlightBooking.Business.Entities.Base;
using FlightBooking.Business.Helpers.CustomAttributes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Business.Entities
{
    [BsonCollection("BookedFlights")]
    public class BookedFlight : BaseEntity
    {
        public string UserId { get; set; }
        public string FlightId { get; set; }
        [BsonIgnore]
        public Flight Flight { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
