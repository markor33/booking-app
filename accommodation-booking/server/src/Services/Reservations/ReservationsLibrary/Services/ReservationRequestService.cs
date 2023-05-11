using Reservations.API.Infrasructure;
using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace ReservationsLibrary.Services
{
    public class ReservationRequestService : IReservationRequestService
    {
        private readonly IReservationRequestRepository _reservationRequestRepository;
        private readonly IAccommodationService _accommodationService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;

        public ReservationRequestService(IReservationRequestRepository reservationRequestRepository,
                                        IAccommodationService accommodationService,
                                        IReservationRepository reservationRepository,
                                        IAccommodationRepository accommodationRepository)
        {
            _reservationRequestRepository = reservationRequestRepository;
            _accommodationService = accommodationService;
            _reservationRepository = reservationRepository;
            _accommodationRepository = accommodationRepository;
        }

        public List<ReservationRequest> GetByUser(Guid userId, string role)
        {
            if(role == "HOST")
                return _reservationRequestRepository.GetByHost(userId);
            return _reservationRequestRepository.GetByGuest(userId);
        } 

        public void ApproveRequest(Guid requestId)
        {
            var request = _reservationRequestRepository.GetById(requestId);
            _reservationRepository.Create(new Reservation(request));
            ChangeStatus(request, ReservationRequestStatus.APPROVED);
            DeclineOverLapped(request.Period, request.AccommodationId);
        }

        public void DeclineRequest(Guid requestId)
        {
            var request = _reservationRequestRepository.GetById(requestId);
            ChangeStatus(request, ReservationRequestStatus.DECLINED);
        }

        public ReservationRequest Create(ReservationRequest request)
        {
            request.Accommodation = _accommodationRepository.GetById(request.AccommodationId);
            var req = _reservationRequestRepository.Create(request);
            if (_accommodationService.IsAutoConfirmation(req.AccommodationId))
            {
                _reservationRepository.Create(new Reservation(req));
                ChangeStatus(req, ReservationRequestStatus.APPROVED);
            }

            return req;
        }
        public void DeleteRequest(Guid requestId) => _reservationRequestRepository.Delete(requestId);

        public void DeleteAllRequestsByGuest(Guid guestId) => _reservationRequestRepository.DeleteAllRequestsByGuest(guestId);

        public void DeleteReservationRequestsByHost(Guid hostId) => _reservationRequestRepository.DeleteReservationRequestsByHost(hostId);

        public void ChangeStatus(ReservationRequest request, ReservationRequestStatus status)
        {
            request.Status = status;
            _reservationRequestRepository.Update(request);
        }
        public void DeclineOverLapped(DateRange range, Guid accommodationId)
        {
            foreach(ReservationRequest rq in _reservationRequestRepository.GetOverLapped(range, accommodationId))
            {
                ChangeStatus(rq, ReservationRequestStatus.DECLINED);
            }
        } 

    }
}
