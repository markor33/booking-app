using EventBus.NET.Integration;
using Ratings.IntegrationEvents;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace RatingsLibrary.IntegrationEvents
{
    public class HostRegisteredIntegrationEventHandler : IIntegrationEventHandler<HostRegisteredIntegrationEvent>
    {
        private readonly IProminentHostRepository _repository;

        public HostRegisteredIntegrationEventHandler(IProminentHostRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(HostRegisteredIntegrationEvent @event)
        {
            var prominent = new ProminentHost { HostId = @event.HostId };
            await _repository.CreateAsync(prominent);
        }
    }
}
