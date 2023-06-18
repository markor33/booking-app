using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class AccommodationCreatedIntegrationEventHandler : IIntegrationEventHandler<AccommodationCreatedIntegrationEvent>
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationCreatedIntegrationEventHandler(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public async Task HandleAsync(AccommodationCreatedIntegrationEvent @event)
        {
            await _accommodationRepository.Create(
                new Accommodation(@event.AccommodationId, @event.HostId, @event.Name, @event.Location.ToString(), @event.Photo));
        }
    }
}
