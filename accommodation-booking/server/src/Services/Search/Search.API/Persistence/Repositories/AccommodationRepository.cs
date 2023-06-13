using MongoDB.Driver;
using Search.API.Models;
using Search.API.Persistence.Settings;

namespace Search.API.Persistence.Repositories
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly IMongoCollection<Accommodation> _accommodations;

        public AccommodationRepository(IMongoDbFactory mongoDbFactory)
        {
            _accommodations = mongoDbFactory.GetCollection<Accommodation>("accommodations");
        }

        public async Task<Accommodation> GetByIdAsync(Guid id)
        {
            var filter = Builders<Accommodation>.Filter.Eq(fr => fr.Id, id);
            return await (await _accommodations.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Accommodation accommodation)
        {
            await _accommodations.InsertOneAsync(accommodation);
        }

        public async Task UpdateAsync(Accommodation accommodation)
        {
            var filter = Builders<Accommodation>.Filter.Eq(fr => fr.Id, accommodation.Id);
            await _accommodations.ReplaceOneAsync(filter, accommodation);
        }

        public async Task<List<Accommodation>> Search(string location, int numOfGuests, DateTime startDate, DateTime endDate)
        {
            var locationFilter = Builders<Accommodation>.Filter.Or(
                Builders<Accommodation>.Filter.Eq(a => a.Location.Country, location),
                Builders<Accommodation>.Filter.Eq(a => a.Location.City, location));

            var guestsFilter = Builders<Accommodation>.Filter.And(
                Builders<Accommodation>.Filter.Lte(a => a.MinGuests, numOfGuests),
                Builders<Accommodation>.Filter.Gte(a => a.MaxGuests, numOfGuests)
            );

            var dateFilter = Builders<Accommodation>.Filter.Not(
                 Builders<Accommodation>.Filter.ElemMatch(a => a.Reservations, r =>
                        r.Period.Start <= endDate && r.Period.End >= startDate)
            );

            var combinedFilter = Builders<Accommodation>.Filter.And(locationFilter, guestsFilter, dateFilter);

            var accommodations = await _accommodations.FindAsync(combinedFilter);

            return accommodations.ToList();
        }
    }
}
