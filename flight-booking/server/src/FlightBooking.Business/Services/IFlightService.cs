using FlightBooking.Business.Entities;

namespace FlightBooking.Business.Services
{
    public interface IFlightService
    {
        public Task<List<Flight>> GetAllAsync();
        public Task<Flight> GetByIdAsync(string id);
        public Task<List<Flight>> SearchAsync(DateTime date, string origin, string destination, int numberOfPassengers);
        public Task<Flight> CreateAsync(Flight flight);
        public Task UpdateAsync(Flight flight);
        public Task DeleteAsync(string id);
    }
}
