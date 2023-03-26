using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Persistence.Repositories.Base;
using FlightBooking.Persistence.Settings;

namespace FlightBooking.Persistence.Repositories
{
    public class FlightRepository: Repository<Flight>, IFlightRepository
    {

        public FlightRepository(IMongoDbFactory mongoDbFactory): base(mongoDbFactory) { }

    }
}
