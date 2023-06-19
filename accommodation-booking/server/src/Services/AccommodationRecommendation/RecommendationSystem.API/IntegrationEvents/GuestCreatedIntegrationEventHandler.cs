using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class GuestCreatedIntegrationEventHandler : IIntegrationEventHandler<GuestCreatedIntegrationEvent>
    {
        private readonly IGuestRepository _guestRepository;

        public GuestCreatedIntegrationEventHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task HandleAsync(GuestCreatedIntegrationEvent @event)
        {
            await _guestRepository.Create(new Guest(@event.GuestId, @event.Name));
        }
    }
}
