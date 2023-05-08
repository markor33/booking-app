using Reservations.API.Infrasructure;

namespace ReservationsLibrary.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public int NumOfCanceledReservationForGuest(Guid guestId)
        {
            return _reservationRepository.NumOfCanceledReservationForGuest(guestId);
        }

        public void CancelReservation(Guid reservationId)
        {
            var res = _reservationRepository.GetById(reservationId);
            res.Canceled = true;
            _reservationRepository.Update(res);
        }
    }
}
