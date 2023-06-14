using EventBus.NET.Integration;
using EventBus.NET.Integration.EventBus;
using ReservationsLibrary.Services;

namespace ReservationsLibrary.IntegrationEvents
{
    public class DeleteGuestRequestIntegrationEventHandler : IIntegrationEventHandler<DeleteGuestRequestIntegrationEvent>
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationRequestService _reservationRequestService;
        private readonly IEventBus _eventBus;

        public DeleteGuestRequestIntegrationEventHandler(
            IReservationService reservationService, 
            IReservationRequestService reservationRequestService,
            IEventBus eventBus)
        {
            _reservationService = reservationService;
            _reservationRequestService = reservationRequestService;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(DeleteGuestRequestIntegrationEvent @event)
        {
            var result = _reservationService.ActiveGuestReservations(@event.GuestId);
            if (result)
            {
                _eventBus.Publish(new GuestReservationsDeleteUnsuccessfulIntegrationEvent(@event.GuestId));
                return;
            }

            _reservationRequestService.DeleteAllRequestsByGuest(@event.GuestId);
            _eventBus.Publish(new GuestReservationsDeletedIntegrationEvent(@event.GuestId));
        }
    }
}
