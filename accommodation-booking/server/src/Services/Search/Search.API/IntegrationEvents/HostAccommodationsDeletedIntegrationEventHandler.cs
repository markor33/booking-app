using EventBus.NET.Integration;
using EventBus.NET.Integration.EventBus;
using Search.API.Persistence.Repositories;

namespace Search.API.IntegrationEvents
{
    public class HostAccommodationsDeletedIntegrationEventHandler
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IEventBus _eventBus;

        public HostAccommodationsDeletedIntegrationEventHandler(
            IAccommodationRepository accommodationRepository,
            IEventBus eventBus)
        {
            _accommodationRepository = accommodationRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(HostAccommodationsDeletedIntegrationEvent @event)
        {
            var result = await _accommodationRepository.DeleteByHostAsync(@event.HostId);
            if (result)
            {
                _eventBus.Publish(new HostSearchAccommodationsDeletedIntegrationEvent(@event.HostId));
            }
            else
            {
                _eventBus.Publish(new HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent(@event.HostId));
            }
        }
    }
}
