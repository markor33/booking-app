using FlightBooking.Business.Entities;

namespace FlightBooking.Business.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllAsync();
        public Task<User> GetByIdAsync(string id);
        public Task UpdateAsync(User user);

    }
}
