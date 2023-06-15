using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace RatingsLibrary.Services
{
    public class HostRatingService : IHostRatingService
    {
        private readonly IHostRatingRepository _hostRatingRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IProminentHostService _prominentHostService;

        public HostRatingService(IHostRatingRepository hostRatingRepository, IReservationRepository reservationRepository, 
                                 IProminentHostService prominentHostService)
        {
            _hostRatingRepository = hostRatingRepository;
            _reservationRepository = reservationRepository;
            _prominentHostService = prominentHostService;
        }

        public bool CreateOrEditHostRating(HostRating hostRating)
        {
            if (EditHostRatingIfExits(hostRating))
                return true;
            if (!CheckIfCanRate(hostRating.GuestId, hostRating.HostId))
                return false;
            _hostRatingRepository.Create(hostRating);
            _prominentHostService.UpdateGradeAcceptableForHost(hostRating.HostId);
            return true;
        }

        public void DeleteHostRating(Guid hostRatingId)
        {
           _hostRatingRepository.Delete(hostRatingId);
        }

        public List<HostRating> GetAllByHost(Guid hostId)
        {
            return _hostRatingRepository.GetAllByHost(hostId);
        }

        public double GetAverageByHost(Guid hostId)
        {
            return _hostRatingRepository.GetAverageGradeByHost(hostId);
        }

        private bool CheckIfCanRate(Guid guestId, Guid hostId)
        {
            var res = _reservationRepository.GetReservationByGuestAndHostInPast(guestId, hostId);
            if(res != null)
                return true;
            return false;
        }

        private bool EditHostRatingIfExits(HostRating hostRating)
        {
            var rating = _hostRatingRepository.GetByGuestAndHost(hostRating.GuestId, hostRating.HostId);
            if (rating != null)
            {
                rating.Grade = hostRating.Grade;
                rating.DateTimeOfGrade = DateTime.Now;
                _hostRatingRepository.Update(rating);
                _prominentHostService.UpdateGradeAcceptableForHost(hostRating.HostId);
                return true;
            }
            return false;
        }
    }
}
