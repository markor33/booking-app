using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;

namespace RatingsLibrary.Repository
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Reservation GetReservationByGuestAndHostInPast(Guid guestId, Guid hostId);
        Reservation GetReservationByGuestAndAccommodationInPast(Guid guestId, Guid accommodationId);
        bool CheckReservationCountInPastForHost(Guid hostId);
        bool CheckReservationDurationInPastForHost(Guid hostId);
        bool CheckCancelationRateLessThenFive(Guid hostId);
    }
}
