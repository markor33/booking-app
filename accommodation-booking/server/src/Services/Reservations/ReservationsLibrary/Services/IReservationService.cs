namespace ReservationsLibrary.Services
{
    public interface IReservationService
    {
        public int NumOfCanceledReservationForGuest(Guid guestId);
        public void CancelReservation(Guid reservationId);
    }
}
