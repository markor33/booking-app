using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;

namespace FlightBooking.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFlightService _flightService;
        private readonly IBookedFlightService _bookedFlightService;

        public UserService(IUserRepository userRepository, IFlightService flightService, IBookedFlightService bookedFlightService)
        {
            _userRepository = userRepository;
            _flightService = flightService;
            _bookedFlightService = bookedFlightService;
        }

        public Task<List<User>> GetAllAsync() => _userRepository.GetAllAsync();

        public Task<User> GetByIdAsync(string id) => _userRepository.GetByIdAsync(id);

        public Task UpdateAsync(User user) => _userRepository.UpdateAsync(user);

    }
}
