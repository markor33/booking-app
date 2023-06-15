using Reservations.API.Infrasructure;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;
using FluentResults;
using EventBus.NET.Integration.EventBus;
using ReservationsLibrary.IntegrationEvents;
using ReservationsLibrary.IntegrationEvents.Notifications;

namespace ReservationsLibrary.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IEventBus _eventBus;

        public ReservationService(
            IReservationRepository reservationRepository,
            IEventBus eventBus)
        {
            _reservationRepository = reservationRepository;
            _eventBus = eventBus;
        }

        public int NumOfCanceledReservationForGuest(Guid guestId) => _reservationRepository.NumOfCanceledReservationForGuest(guestId);

        public bool ActiveGuestReservations(Guid guestId) => _reservationRepository.ActiveGuestReservations(guestId);

        public bool ActiveHostReservations(Guid guestId) => _reservationRepository.ActiveHostReservations(guestId);
 
        public void DeleteAllReservationsByGuest(Guid guestId) => _reservationRepository.DeleteAllReservationsByGuest(guestId);

        public void DeleteReservationsByHost(Guid hostId) => _reservationRepository.DeleteReservationsByHost(hostId);

        public bool IsOverLappedByAccomodation(DateRange range, Guid accommodationId) => _reservationRepository.IsOverLappedByAccomodation(range, accommodationId);

        public Result CancelReservation(Guid reservationId)
        {
            var res = _reservationRepository.GetById(reservationId);
            var cancellationDeadline = res.Period.Start.AddHours(-24);
            if (DateTime.Now < cancellationDeadline)
            {
                res.Canceled = true;
                _reservationRepository.Update(res);
                _eventBus.Publish(new ReservationCanceledIntegrationEvent(res.AccommodationId, res.Id));
                _eventBus.Publish(new ReservationCanceledNotification(res.Accommodation.HostId, res.AccommodationId, res.Id));
                return Result.Ok();
            }
            return Result.Fail("Cancel failed");
        }

        public List<Reservation> GetByUser(Guid userId, string role)
        {
            if (role == "HOST")
                return _reservationRepository.GetByHost(userId);
            return _reservationRepository.GetByGuest(userId);
        }
    }
}
