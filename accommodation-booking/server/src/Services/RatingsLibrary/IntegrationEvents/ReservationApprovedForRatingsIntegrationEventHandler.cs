using EventBus.NET.Integration;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace RatingsLibrary.IntegrationEvents
{
    public class ReservationApprovedForRatingsIntegrationEventHandler : IIntegrationEventHandler<ReservationApprovedForRatingsIntegrationEvent>
    {
        private readonly IReservationRepository _repository;

        public ReservationApprovedForRatingsIntegrationEventHandler(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(ReservationApprovedForRatingsIntegrationEvent @event)
        {
            var res = new Reservation
            {
                Id = @event.ReservationId,
                HostId = @event.HostId,
                GuestId = @event.GuestId,
                Period = @event.Period,
                AccommodationId = @event.AccommodationId,
                Canceled = @event.Canceled
            };

            await _repository.CreateAsync(res);
        }
    }
}
