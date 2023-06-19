using EventBus.NET.Integration;
using EventBus.NET.Integration.EventBus;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class HostSearchAccommodationsDeleteUnsuccessfulIntegrationEventHandler : IIntegrationEventHandler<HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IEventBus _eventBus;

        public HostSearchAccommodationsDeleteUnsuccessfulIntegrationEventHandler(IAccommodationRepository accommodationRepository, IEventBus eventBus)
        {
            _accommodationRepository = accommodationRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent @event)
        {
            await _accommodationRepository.SetDeleteByHost(@event.HostId, false);
            _eventBus.Publish(new HostAccommodationsDeleteFromRecommendationSystemUnsuccessfulIntegrationEvent(@event.HostId));
        }
    }
}
