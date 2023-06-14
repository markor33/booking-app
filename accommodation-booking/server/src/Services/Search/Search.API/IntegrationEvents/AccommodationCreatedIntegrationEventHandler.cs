using EventBus.NET.Integration;
using Search.API.Models;
using Search.API.Persistence.Repositories;

namespace Search.API.IntegrationEvents
{
    public class AccommodationCreatedIntegrationEventHandler : IIntegrationEventHandler<AccommodationCreatedIntegrationEvent>
    {
        private readonly IAccommodationRepository _repository;

        public AccommodationCreatedIntegrationEventHandler(IAccommodationRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(AccommodationCreatedIntegrationEvent @event)
        {
            var accommodation = new Accommodation(
                @event.AccommodationId,
                @event.HostId,
                @event.Name,
                @event.Description,
                @event.Location,
                @event.MinGuests,
                @event.MaxGuests,
                @event.Photo,
                @event.Benefits,
                @event.PriceType,
                @event.GeneralPrice,
                @event.WeekendIncrease);

            await _repository.CreateAsync(accommodation);
        }
    }
}
