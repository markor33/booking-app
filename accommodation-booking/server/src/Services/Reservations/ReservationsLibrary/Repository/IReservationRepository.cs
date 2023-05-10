using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure
{
    public interface IReservationRepository : IEntityRepository<Reservation>
    {
        Reservation GetById(Guid resId);
        public int NumOfCanceledReservationForGuest(Guid guestId);
    }
}
