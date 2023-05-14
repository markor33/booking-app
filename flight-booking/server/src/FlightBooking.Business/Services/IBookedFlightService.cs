using FlightBooking.Business.Entities;

namespace FlightBooking.Business.Services
{
    public interface IBookedFlightService
    {
        public Task<List<BookedFlight>> GetAllAsync();
        public Task<BookedFlight> GetByIdAsync(string id);
        public Task<BookedFlight> CreateAsync(BookedFlight flight);
        public Task<List<BookedFlight>> GetByUserIdAsync(string userId);
    }
}
