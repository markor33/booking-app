using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class ReservationCanceledIntegrationEventHandler : IIntegrationEventHandler<ReservationCanceledIntegrationEvent>
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationCanceledIntegrationEventHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task HandleAsync(ReservationCanceledIntegrationEvent @event)
        {
            await _reservationRepository.Delete(new Reservation(@event.ReservationId, @event.AccommodationId, @event.GuestId));
        }
    }
}
