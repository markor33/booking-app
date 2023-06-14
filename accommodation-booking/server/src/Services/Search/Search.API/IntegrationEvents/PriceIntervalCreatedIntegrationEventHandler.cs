using EventBus.NET.Integration;
using Search.API.Models;
using Search.API.Persistence.Repositories;

namespace Search.API.IntegrationEvents
{
    public class PriceIntervalCreatedIntegrationEventHandler : IIntegrationEventHandler<PriceIntervalCreatedIntegrationEvent>
    {
        private readonly IAccommodationRepository _repository;

        public PriceIntervalCreatedIntegrationEventHandler(IAccommodationRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(PriceIntervalCreatedIntegrationEvent @event)
        {
            var accommodation = await _repository.GetByIdAsync(@event.AccommodationId);

            var priceInterval = new PriceInterval(@event.PriceIntervalId, @event.Amount, new DateRange(@event.Start, @event.End));
            accommodation.AddPriceInterval(priceInterval);

            await _repository.UpdateAsync(accommodation);
        }
    }
}
