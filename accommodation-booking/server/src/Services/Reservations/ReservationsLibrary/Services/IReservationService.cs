using FluentResults;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace ReservationsLibrary.Services
{
    public interface IReservationService
    {
        public int NumOfCanceledReservationForGuest(Guid guestId);
        public Result CancelReservation(Guid reservationId);
        public bool ActiveGuestReservations(Guid guestId);
        public bool ActiveHostReservations(Guid guestId);
        public void DeleteAllReservationsByGuest(Guid guestId);
        public void DeleteReservationsByHost(Guid hostId);
        public bool IsOverLappedByAccomodation(DateRange range, Guid accommodationId);
        public List<Reservation> GetByUser(Guid userId, string role);
    }
}
