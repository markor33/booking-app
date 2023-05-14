using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace Reservations.API.Infrasructure
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Reservation GetById(Guid resId);
        public int NumOfCanceledReservationForGuest(Guid guestId);
        public bool ActiveGuestReservations(Guid guestId);
        public bool ActiveHostReservations(Guid hostId);
        public void DeleteAllReservationsByGuest(Guid guestId);
        public void DeleteReservationsByHost(Guid hostId);
        public bool IsOverLappedByAccomodation(DateRange range, Guid accommodationId);
        public List<Reservation> GetByHost(Guid hostId);
        public List<Reservation> GetByGuest(Guid guestId);
    }
}
