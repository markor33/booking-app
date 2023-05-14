using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Persistence.Repositories.Base;
using FlightBooking.Persistence.Settings;
using MongoDB.Driver;

namespace FlightBooking.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(IMongoDbFactory mongoDbFactory) : base(mongoDbFactory) { }

        public async Task<User> GetByAppUserIdAsync(string appUserId)
        {
            var filter = Builders<User>.Filter.Eq(e => e.AppUserId, Guid.Parse(appUserId));
            return await(await _collection.FindAsync(filter)).FirstOrDefaultAsync();
        }
    }
}
