using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;

namespace RatingsLibrary.Repository
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Reservation GetReservationByGuestAndHostInPast(Guid guestId, Guid hostId);
        Reservation GetReservationByGuestAndAccommodationInPast(Guid guestId, Guid accommodationId);
        bool CheckCancelationRateLessThenFive(Guid hostId);
        Task<Reservation> CancelAsync(Guid id);
        Task<Reservation> CreateAsync(Reservation res);
    }
}
