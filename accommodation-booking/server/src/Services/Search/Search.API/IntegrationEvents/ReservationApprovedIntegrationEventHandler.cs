using EventBus.NET.Integration;
using Search.API.Models;
using Search.API.Persistence.Repositories;

namespace Search.API.IntegrationEvents
{
    public class ReservationApprovedIntegrationEventHandler : IIntegrationEventHandler<ReservationApprovedIntegrationEvent>
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public ReservationApprovedIntegrationEventHandler(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public async Task HandleAsync(ReservationApprovedIntegrationEvent @event)
        {
            var accommodation = await _accommodationRepository.GetByIdAsync(@event.AccommodationId);

            var reservation = new Reservation(@event.ReservationId, @event.GuestId, @event.Period);
            accommodation.AddReservation(reservation);

            await _accommodationRepository.UpdateAsync(accommodation);
        }
    }
}
