using Reservations.API.Infrasructure;
using ReservationsLibrary.Models;

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
            var cancellationDeadline = res.Period.Start.AddHours(-24);
            if (DateTime.Now < cancellationDeadline)
                res.Canceled = true;
            _reservationRepository.Update(res);
        }
    }
}
