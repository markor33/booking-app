using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories.Base;

namespace FlightBooking.Business.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Task<List<Flight>> SearchAsync(DateTime date, string origin, string destination, int numberOfPassengers);
    }

}
