using RatingsLibrary.Repository;

namespace RatingsLibrary.Services
{
    public class ProminentHostService : IProminentHostService
    {
        private readonly IProminentHostRepository _prominentHostRepository;
        private readonly IHostRatingRepository _hostRatingRepository;
        private readonly IReservationRepository _reservationRepository;

        public ProminentHostService(IProminentHostRepository prominentHostRepository, IHostRatingRepository ratingRepository,
                                IReservationRepository reservationRepository)
        {
            _prominentHostRepository = prominentHostRepository;
            _hostRatingRepository = ratingRepository;
            _reservationRepository = reservationRepository;
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

        public void UpdateDurationOfReservationsAcceptableForHost(Guid hostId)
        {
            var prominentHost = _prominentHostRepository.GetByHost(hostId);
            if (prominentHost.IsDurationOfReservationsAcceptable)
                return;
            if (!_reservationRepository.CheckReservationDurationInPastForHost(hostId))
                return;
            prominentHost.IsDurationOfReservationsAcceptable = true;
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

        public void UpdateHasFiveReservationsForHost(Guid hostId)
        {
            var prominentHost = _prominentHostRepository.GetByHost(hostId);
            if (prominentHost.HasFiveReservations)
                return;
            if (!_reservationRepository.CheckReservationCountInPastForHost(hostId))
                return;
            prominentHost.HasFiveReservations = true;
            _prominentHostRepository.Update(prominentHost);
        }
        
        public bool IsHostProminent(Guid hostId)
        {
           var prominentHost = _prominentHostRepository.GetByHost(hostId);
           return prominentHost.IsHostProminent;
        }
    }
}
