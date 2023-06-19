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

        public async Task<Accommodation> CheckAvailability(CheckAvailabilityArgs args)
        {
            var idFilter = Builders<Accommodation>.Filter.Eq(fr => fr.Id, args.Id);

            var guestsFilter = Builders<Accommodation>.Filter.And(
                Builders<Accommodation>.Filter.Lte(a => a.MinGuests, args.NumOfGuests),
                Builders<Accommodation>.Filter.Gte(a => a.MaxGuests, args.NumOfGuests)
            );

            var dateFilter = Builders<Accommodation>.Filter.Not(
                 Builders<Accommodation>.Filter.ElemMatch(a => a.Reservations, r =>
                        r.Period.Start <= args.End && r.Period.End >= args.Start)
            );

            var combinedFilter = Builders<Accommodation>.Filter.And(idFilter, guestsFilter, dateFilter);

            var accommodation = await (await _accommodations.FindAsync(combinedFilter)).FirstOrDefaultAsync();

            return accommodation;
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

        public async Task<List<Accommodation>> Search(SearchArgs searchArgs)
        {
            var locationFilter = Builders<Accommodation>.Filter.Or(
                Builders<Accommodation>.Filter.Eq(a => a.Location.Country, searchArgs.Location),
                Builders<Accommodation>.Filter.Eq(a => a.Location.City, searchArgs.Location));

            var guestsFilter = Builders<Accommodation>.Filter.And(
                Builders<Accommodation>.Filter.Lte(a => a.MinGuests, searchArgs.NumOfGuests),
                Builders<Accommodation>.Filter.Gte(a => a.MaxGuests, searchArgs.NumOfGuests)
            );

            var dateFilter = Builders<Accommodation>.Filter.Not(
                 Builders<Accommodation>.Filter.ElemMatch(a => a.Reservations, r =>
                        r.Period.Start <= searchArgs.End && r.Period.End >= searchArgs.Start)
            );

            List<FilterDefinition<Accommodation>> benefitFilters = searchArgs.FilterArgs.Benefits.Select(benefitGuid =>
                Builders<Accommodation>.Filter.ElemMatch(a => a.Benefits, b => b.Id == benefitGuid)).ToList();

            var benefitFilter = Builders<Accommodation>.Filter.And(benefitFilters);

            var combinedFilter = Builders<Accommodation>.Filter.And(locationFilter, guestsFilter, dateFilter);

            var accommodations = await (await _accommodations.FindAsync(combinedFilter)).ToListAsync();

            return accommodations.ToList();
        }
       
    }
}
