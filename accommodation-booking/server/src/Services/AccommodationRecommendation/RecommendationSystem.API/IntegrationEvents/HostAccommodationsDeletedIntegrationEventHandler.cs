using EventBus.NET.Integration;
using EventBus.NET.Integration.EventBus;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class HostAccommodationsDeletedIntegrationEventHandler : IIntegrationEventHandler<HostAccommodationsDeletedIntegrationEvent>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IEventBus _eventBus;

        public HostAccommodationsDeletedIntegrationEventHandler(IAccommodationRepository accommodationRepository, IEventBus eventBus)
        {
            _accommodationRepository = accommodationRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(HostAccommodationsDeletedIntegrationEvent @event)
        {
            var result = await _accommodationRepository.SetDeleteByHost(@event.HostId, true);
            if (!result)
                _eventBus.Publish(new HostAccommodationsDeleteFromRecommendationSystemUnsuccessfulIntegrationEvent(@event.HostId));
            _eventBus.Publish(new HostAccommodationsDeletedFromRecommendationSystemIntegrationEvent(@event.HostId));
        }
    }
}
