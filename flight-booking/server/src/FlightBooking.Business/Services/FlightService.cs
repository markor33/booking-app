using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
        public Task UpdateAsync(Flight flight) => _flightRepository.UpdateAsync(flight);
       
    }
}
