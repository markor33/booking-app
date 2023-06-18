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

        public void DeleteHostRating(Guid reservationId)
        {
            var rating = _hostRatingRepository.GetByReservationId(reservationId);
           _hostRatingRepository.Delete(rating.Id);
        }

        public List<HostRating> GetAllByHost(Guid hostId)
        {
            return _hostRatingRepository.GetAllByHost(hostId);
        }

        public double GetAverageByHost(Guid hostId)
        {
            return _hostRatingRepository.GetAverageGradeByHost(hostId);
        }

        public bool CheckIfCanRate(Guid guestId, Guid hostId)
        {
            var res = _reservationRepository.GetReservationByGuestAndHostInPast(guestId, hostId);
            if(res != null)
                return true;
            return false;
        }

        public List<int> GetGradesByGuest(Guid guestId, string role)
        {
            if(role == "GUEST")
                return _hostRatingRepository.GetGradesByGuest(guestId);
            else
                return _hostRatingRepository.GetGradesByHost(guestId);
        }

        private bool EditHostRatingIfExits(HostRating hostRating)
        {
            var rating = _hostRatingRepository.GetByReservationId(hostRating.ReservationId);
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

        public List<int> GetAllGradesByGuest(Guid guestId)
        {
            throw new NotImplementedException();
        }
    }
}
