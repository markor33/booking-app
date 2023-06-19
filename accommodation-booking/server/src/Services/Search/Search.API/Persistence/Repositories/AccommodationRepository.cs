using MongoDB.Driver;
using Search.API.DTO;
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

        public async Task<bool> DeleteByHostAsync(Guid hostId)
        {
            var filter = Builders<Accommodation>.Filter.Eq(fr => fr.HostId, hostId);
            var update = Builders<Accommodation>.Update.Set(fr => fr.IsDeleted, true);

            try
            {
                var result = await _accommodations.UpdateManyAsync(filter, update);
                return result.IsAcknowledged;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteGuestReservations(Guid guestId)
        {
            var filter = Builders<Accommodation>.Filter.ElemMatch(acc => acc.Reservations, res => res.GuestId == guestId);
            var update = Builders<Accommodation>.Update.Set("Reservations.$.IsDeleted", true);

            try
            {
                var result = await _accommodations.UpdateManyAsync(filter, update);
                return result.IsAcknowledged;
            }
            catch (Exception)
            {
                return false;
            }
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
                        r.Period.Start <= startDate && r.Period.End >= endDate)
            );


            var combinedFilter = Builders<Accommodation>.Filter.And(locationFilter, guestsFilter, dateFilter);

            var accommodations = await _accommodations.FindAsync(combinedFilter);

            return accommodations.ToList();
        }
    }
}
