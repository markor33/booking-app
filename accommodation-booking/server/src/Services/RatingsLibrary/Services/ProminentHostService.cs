using EventBus.NET.Integration.EventBus;
using RatingsLibrary.IntegrationEvents.Notifications;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace RatingsLibrary.Services
{
    public class ProminentHostService : IProminentHostService
    {
        private readonly IProminentHostRepository _prominentHostRepository;
        private readonly IHostRatingRepository _hostRatingRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationRatingRepository _accommodationRatingRepository;
        private readonly IEventBus _eventBus;

        public ProminentHostService(
            IProminentHostRepository prominentHostRepository, IHostRatingRepository ratingRepository,
                                IReservationRepository reservationRepository, 
                                IAccommodationRatingRepository accommodationRatingRepository,
                                IEventBus eventBus)
        {
            _prominentHostRepository = prominentHostRepository;
            _hostRatingRepository = ratingRepository;
            _reservationRepository = reservationRepository;
            _accommodationRatingRepository = accommodationRatingRepository;
            _eventBus = eventBus;
        }

        public void UpdateCancellationRateAcceptableForHost(Guid hostId)
        {
            var prominentHost = _prominentHostRepository.GetByHost(hostId);
            var currentState = prominentHost.IsHostProminent;
            if (!_reservationRepository.CheckCancelationRateLessThenFive(hostId))
            {
                prominentHost.IsCancellationRateAcceptable = false;
            }
            else
            {
                prominentHost.IsCancellationRateAcceptable = true;
            }
            _prominentHostRepository.Update(prominentHost);

            if (currentState != prominentHost.IsHostProminent)
                _eventBus.Publish(new HostProminentStatusChanged(hostId, prominentHost.IsHostProminent));
        }

        public void UpdateGradeAcceptableForHost(Guid hostId)
        {
            var prominentHost = _prominentHostRepository.GetByHost(hostId);
            var currentState = prominentHost.IsHostProminent;
            if (_hostRatingRepository.GetAverageGradeByHost(hostId) <= 4.7)
            {
                prominentHost.IsGradeAcceptable = false;
            }
            else
            {
                prominentHost.IsGradeAcceptable = true;
            }
            _prominentHostRepository.Update(prominentHost);

            if (currentState != prominentHost.IsHostProminent)
                _eventBus.Publish(new HostProminentStatusChanged(hostId, prominentHost.IsHostProminent));
        }   
        public bool IsHostProminent(Guid hostId)
        {
           var prominentHost = _prominentHostRepository.GetByHost(hostId);
           return prominentHost.IsHostProminent;
        }
        public ProminentHostStats GetHostProminentStats(Guid hostId, Guid accommId)
        {
            var hostStats = new ProminentHostStats
            {
                IsHostProminent = _prominentHostRepository.GetByHost(hostId).IsHostProminent,
                AvgHostGrade = _hostRatingRepository.GetAverageGradeByHost(hostId),
                AvgAccommGrade = _accommodationRatingRepository.GetAverageGradeByAccommodation(accommId)
            };
            return hostStats;
        }
    }
}
