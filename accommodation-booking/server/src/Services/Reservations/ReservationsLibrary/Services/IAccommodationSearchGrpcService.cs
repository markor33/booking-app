using ReservationsLibrary.Models;

namespace ReservationsLibrary.Services
{
    public interface IAccommodationSearchGrpcService
    {
        Task AddReservation(Reservation reservation);
        Task DeleteReservation(Reservation reservation);
    }
}
