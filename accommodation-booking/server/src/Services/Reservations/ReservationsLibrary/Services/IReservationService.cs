namespace ReservationsLibrary.Services
{
    public interface IReservationService
    {
        public int NumOfCanceledReservationForGuest(Guid guestId);
        public void CancelReservation(Guid reservationId);
        public bool ActiveGuestReservations(Guid guestId);
        public bool ActiveHostReservations(Guid guestId);
        public void DeleteAllReservationsByGuest(Guid guestId);
        public void DeleteReservationsByHost(Guid hostId);
    }
}
