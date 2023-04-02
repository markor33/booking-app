using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories.Base;

namespace FlightBooking.Business.Repositories
{
    public interface IBookedFlightRepository : IRepository<BookedFlight>
    {
        public Task<List<BookedFlight>> GetByUserIdAsync(string userId);
    }

}
