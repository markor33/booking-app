using EventBus.NET.Integration;
using RatingsLibrary.Repository;
using RatingsLibrary.Services;

namespace RatingsLibrary.IntegrationEvents
{
    public class ReservationCanceledIntegrationEventHandler : IIntegrationEventHandler<ReservationCanceledIntegrationEvent>
    {
        private readonly IReservationRepository _repository;
        private readonly IProminentHostService _prominentHostService;

        public ReservationCanceledIntegrationEventHandler(IReservationRepository repository, IProminentHostService prominentHostService)
        {
            _repository = repository;
            _prominentHostService = prominentHostService;
        }

        public async Task HandleAsync(ReservationCanceledIntegrationEvent @event)
        {
           var res = await _repository.CancelAsync(@event.ReservationId);
           _prominentHostService.UpdateCancellationRateAcceptableForHost(res.HostId);
        }
    }
}
