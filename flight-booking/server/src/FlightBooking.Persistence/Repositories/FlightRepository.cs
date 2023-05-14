using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Persistence.Repositories.Base;
using FlightBooking.Persistence.Settings;
using MongoDB.Driver;

namespace FlightBooking.Persistence.Repositories
{
    public class FlightRepository: Repository<Flight>, IFlightRepository
    {

        public FlightRepository(IMongoDbFactory mongoDbFactory): base(mongoDbFactory) { }

        public async Task<List<Flight>> SearchAsync(DateTime date, string origin, string destination, int numberOfPassengers)
        {
            var endDate = date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var filter = Builders<Flight>.Filter.Gt(f => f.DepartureTime, date);
            filter &= Builders<Flight>.Filter.Lt(f => f.DepartureTime, endDate);
            filter &= Builders<Flight>.Filter.Eq(f => f.Origin, origin);
            filter &= Builders<Flight>.Filter.Eq(f => f.Destination, destination);
            filter &= Builders<Flight>.Filter.Gt(f => f.NumOfAvailableTickets, numberOfPassengers);

            return await(await _collection.FindAsync(filter)).ToListAsync();
        }
    }
}
