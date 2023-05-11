using Reservations.API.Infrasructure;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace ReservationsLibrary.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationSearchGrpcService _accommodationSearchGrpcService;

        public ReservationService(
            IReservationRepository reservationRepository,
            IAccommodationSearchGrpcService accommodationSearchGrpcService)
        {
            _reservationRepository = reservationRepository;
            _accommodationSearchGrpcService = accommodationSearchGrpcService;
        }

        public int NumOfCanceledReservationForGuest(Guid guestId) => _reservationRepository.NumOfCanceledReservationForGuest(guestId);

        public bool ActiveGuestReservations(Guid guestId) => _reservationRepository.ActiveGuestReservations(guestId);

        public bool ActiveHostReservations(Guid guestId) => _reservationRepository.ActiveHostReservations(guestId);
 
        public void DeleteAllReservationsByGuest(Guid guestId) => _reservationRepository.DeleteAllReservationsByGuest(guestId);

        public void DeleteReservationsByHost(Guid hostId) => _reservationRepository.DeleteReservationsByHost(hostId);

        public bool IsOverLappedByAccomodation(DateRange range, Guid accommodationId) => _reservationRepository.IsOverLappedByAccomodation(range, accommodationId);

        public void CancelReservation(Guid reservationId)
        {
            var res = _reservationRepository.GetById(reservationId);
            var cancellationDeadline = res.Period.Start.AddHours(-24);
            if (DateTime.Now < cancellationDeadline)
                res.Canceled = true;
            _reservationRepository.Update(res);
            _accommodationSearchGrpcService.DeleteReservation(res);
        }

    
    }
}
