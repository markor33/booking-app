using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Persistence.Repositories.Base;
using FlightBooking.Persistence.Settings;
using MongoDB.Driver;

namespace FlightBooking.Persistence.Repositories
{
    public class ApiKeyRepository : Repository<ApiKey>, IApiKeyRepository
    {
        public ApiKeyRepository(IMongoDbFactory mongoDbFactory) : base(mongoDbFactory) { }

        public async Task<ApiKey> GetByUser(string userId)
        {
            var filter = Builders<ApiKey>.Filter.Eq(ak => ak.UserId, userId);
            var a =  await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
            return a;
        }

    }
}
