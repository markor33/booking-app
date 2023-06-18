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

        public ProminentHostService(IProminentHostRepository prominentHostRepository, IHostRatingRepository ratingRepository,
                                IReservationRepository reservationRepository, IAccommodationRatingRepository accommodationRatingRepository)
        {
            _prominentHostRepository = prominentHostRepository;
            _hostRatingRepository = ratingRepository;
            _reservationRepository = reservationRepository;
            _accommodationRatingRepository = accommodationRatingRepository;
        }

        public void UpdateCancellationRateAcceptableForHost(Guid hostId)
        {
            var prominentHost = _prominentHostRepository.GetByHost(hostId);
            if (!_reservationRepository.CheckCancelationRateLessThenFive(hostId))
            {
                prominentHost.IsCancellationRateAcceptable = false;
            }
            else
            {
                prominentHost.IsCancellationRateAcceptable = true;
            }

            _prominentHostRepository.Update(prominentHost);
        }

        public void UpdateGradeAcceptableForHost(Guid hostId)
        {
            var prominentHost = _prominentHostRepository.GetByHost(hostId);
            if (_hostRatingRepository.GetAverageGradeByHost(hostId) <= 4.7)
            {
                prominentHost.IsGradeAcceptable = false;
            }
            else
            {
                prominentHost.IsGradeAcceptable = true;
            }
            _prominentHostRepository.Update(prominentHost);
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
