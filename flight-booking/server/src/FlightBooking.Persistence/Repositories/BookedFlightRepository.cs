using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Persistence.Repositories.Base;
using FlightBooking.Persistence.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlightBooking.Persistence.Repositories
{
    public class BookedFlightRepository : Repository<BookedFlight>, IBookedFlightRepository
    {

        public BookedFlightRepository(IMongoDbFactory mongoDbFactory) : base(mongoDbFactory) { }

        public async Task<List<BookedFlight>> GetByUserIdAsync(string userId)
        {
            var filter = Builders<BookedFlight>.Filter.Eq(e => e.UserId, userId);
            return await(await _collection.FindAsync(filter)).ToListAsync();
        }
    }
}
