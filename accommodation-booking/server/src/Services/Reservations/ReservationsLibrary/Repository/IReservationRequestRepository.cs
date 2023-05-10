using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace Reservations.API.Infrasructure
{
    public interface IReservationRequestRepository : IEntityRepository<ReservationRequest>
    {
        public List<ReservationRequest> GetOverLapped(DateRange range, Guid accommodationId);
        public List<ReservationRequest> GetByHost(Guid hostId);
        public List<ReservationRequest> GetByGuest(Guid guestId);
        public void DeleteAllRequestsByGuest(Guid guestId);
    }
}
