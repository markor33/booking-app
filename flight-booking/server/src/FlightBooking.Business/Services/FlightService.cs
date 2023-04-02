using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;

namespace FlightBooking.Business.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public Task<Flight> CreateAsync(Flight flight) => _flightRepository.CreateAsync(flight);

        public Task DeleteAsync(string id) => _flightRepository.DeleteAsync(id);

        public Task<List<Flight>> GetAllAsync() => _flightRepository.GetAllAsync();

        public Task<Flight> GetByIdAsync(string id) => _flightRepository.GetByIdAsync(id);

        public async Task<List<Flight>> SearchAsync(DateTime date, string origin, string destination, int numberOfPassengers)
            => await _flightRepository.SearchAsync(date, origin, destination, numberOfPassengers);

        public Task UpdateAsync(Flight flight) => _flightRepository.UpdateAsync(flight);
       
    }
}
