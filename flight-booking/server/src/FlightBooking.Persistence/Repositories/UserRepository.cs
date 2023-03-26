using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using FlightBooking.Persistence.Repositories.Base;
using FlightBooking.Persistence.Settings;

namespace FlightBooking.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(IMongoDbFactory mongoDbFactory) : base(mongoDbFactory) { }

    }
}
