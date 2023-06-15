using EventBus.NET.Integration;
using EventBus.NET.Integration.EventBus;
using Reservations.API.Infrasructure;
using ReservationsLibrary.Services;

namespace ReservationsLibrary.IntegrationEvents
{
    public class DeleteHostRequestIntegrationEventHandler : IIntegrationEventHandler<DeleteHostRequestIntegrationEvent>
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IReservationService _reservationService;
        private readonly IEventBus _eventBus;

        public DeleteHostRequestIntegrationEventHandler(
            IAccommodationRepository accommodationRepository, 
            IReservationService reservationService,
            IEventBus eventBus)
        {
            _accommodationRepository = accommodationRepository;
            _reservationService = reservationService;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(DeleteHostRequestIntegrationEvent @event)
        {
            var hasActiveReservations = _reservationService.ActiveHostReservations(@event.HostId);
            if (hasActiveReservations)
            {
                _eventBus.Publish(new HostReservationsDeleteUnsuccessfulIntegrationEvent(@event.HostId));
                return;
            }

            var result = await _accommodationRepository.DeleteAccommodationByHost(@event.HostId);
            if (result)
            {
                _eventBus.Publish(new HostReservationsDeletedIntegrationEvent(@event.HostId));
            }
            else
            {
                _eventBus.Publish(new HostReservationsDeleteUnsuccessfulIntegrationEvent(@event.HostId));
            }
        }
    }
}
