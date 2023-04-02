using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;

namespace FlightBooking.Business.Services
{
    public class BookedFlightService : IBookedFlightService
    {
        private readonly IBookedFlightRepository _bookedFlightRepository;
        private readonly IFlightService _flightService;

        public BookedFlightService(IBookedFlightRepository bookedFlightRepository, IFlightService flightService)
        {
            _bookedFlightRepository = bookedFlightRepository;
            _flightService = flightService;
        }

        public async Task<BookedFlight> CreateAsync(BookedFlight bookedFlight)
        {
            var flight = await _flightService.GetByIdAsync(bookedFlight.FlightId);
            flight.NumOfAvailableTickets -= bookedFlight.NumberOfTickets;
            await _flightService.UpdateAsync(flight);
            return await _bookedFlightRepository.CreateAsync(bookedFlight);            
        }

        public Task<List<BookedFlight>> GetAllAsync() => _bookedFlightRepository.GetAllAsync();

        public Task<BookedFlight> GetByIdAsync(string id) => _bookedFlightRepository.GetByIdAsync(id);

        public async Task<List<BookedFlight>> GetByUserIdAsync(string userId) {
            var bookedFlightsForUser = await _bookedFlightRepository.GetByUserIdAsync(userId);
            foreach (BookedFlight b in bookedFlightsForUser)
            {
                b.Flight = await _flightService.GetByIdAsync(b.FlightId);
            }
            return bookedFlightsForUser;
        }
    }
}
