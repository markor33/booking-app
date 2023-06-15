using EventBus.NET.Integration;
using Search.API.Persistence.Repositories;

namespace Search.API.IntegrationEvents
{
    public class ReservationCanceledIntegrationEventHandler : IIntegrationEventHandler<ReservationCanceledIntegrationEvent>
    {
        private readonly IAccommodationRepository _repository;

        public ReservationCanceledIntegrationEventHandler(IAccommodationRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(ReservationCanceledIntegrationEvent @event)
        {
            var accommodation = await _repository.GetByIdAsync(@event.AccommodationId);

            accommodation.RemoveReservation(@event.ReservationId);

            await _repository.UpdateAsync(accommodation);
        }
    }
}
