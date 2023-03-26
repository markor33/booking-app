using MongoDB.Driver;

namespace FlightBooking.Persistence.Settings
{
    public interface IMongoDbFactory
    {
        public IMongoCollection<T> GetCollection<T>();
    }
}
