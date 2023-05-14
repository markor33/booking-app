using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories.Base;

namespace FlightBooking.Business.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByAppUserIdAsync(string appUserId);
    }
}
