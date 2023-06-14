using EventBus.NET.Integration;
using Search.API.Models;
using Search.API.Persistence.Repositories;

namespace Search.API.IntegrationEvents
{
    public class PriceIntervalChangedIntegrationEventHandler : IIntegrationEventHandler<PriceIntervalChangedIntegrationEvent>
    {
        private readonly IAccommodationRepository _repository;

        public PriceIntervalChangedIntegrationEventHandler(IAccommodationRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(PriceIntervalChangedIntegrationEvent @event)
        {
            var accommodation = await _repository.GetByIdAsync(@event.AccommodationId);

            accommodation.UpdatePriceInterval(@event.PriceIntervalId, @event.Amount, new DateRange(@event.Start, @event.End));

            await _repository.UpdateAsync(accommodation);
        }
    }
}
