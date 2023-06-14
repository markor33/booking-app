using EventBus.NET.Integration;
using EventBus.NET.Integration.EventBus;
using Reservations.API.Infrasructure;

namespace ReservationsLibrary.IntegrationEvents
{
    public class HostAccommodationsDeleteUnsuccessfulIntegrationEventHandler : IIntegrationEventHandler<HostAccommodationsDeleteUnsuccessfulIntegrationEvent>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IEventBus _eventBus;

        public HostAccommodationsDeleteUnsuccessfulIntegrationEventHandler(
            IAccommodationRepository accommodationRepository,
            IEventBus eventBus)
        {
            _accommodationRepository = accommodationRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(HostAccommodationsDeleteUnsuccessfulIntegrationEvent @event)
        {
            var accommodations = await _accommodationRepository.GetByHost(@event.HostId);

            foreach (var accommodation in accommodations)
                accommodation.SetDelete(false);

            await _accommodationRepository.UnitOfWork.SaveEntitiesAsync();
            _eventBus.Publish(new HostReservationsDeleteUnsuccessfulIntegrationEvent(@event.HostId));
        }

    }
}
