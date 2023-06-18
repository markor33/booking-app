using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class ReservationApprovedIntegrationEventHandler : IIntegrationEventHandler<ReservationApprovedIntegrationEvent>
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationApprovedIntegrationEventHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task HandleAsync(ReservationApprovedIntegrationEvent @event)
        {
            await _reservationRepository.Create(new Reservation(@event.ReservationId, @event.AccommodationId, @event.GuestId));
        }
    }
}
