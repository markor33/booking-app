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

        public int NumOfCanceledReservationForGuest(Guid guestId) => _reservationRepository.NumOfCanceledReservationForGuest(guestId);

        public bool ActiveGuestReservations(Guid guestId) => _reservationRepository.ActiveGuestReservations(guestId);

        public bool ActiveHostReservations(Guid guestId) => _reservationRepository.ActiveHostReservations(guestId);
 
        public void DeleteAllReservationsByGuest(Guid guestId) => _reservationRepository.DeleteAllReservationsByGuest(guestId);

        public void DeleteReservationsByHost(Guid hostId) => _reservationRepository.DeleteReservationsByHost(hostId);

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
