using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FlightBooking.Business.Entities.Base
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
