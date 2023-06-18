using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace RatingsLibrary.Services
{
    public class AccommodationRatingService : IAccommodationRatingService
    {
        private readonly IAccommodationRatingRepository _accommodationRatingRepository;
        private readonly IReservationRepository _reservationRepository;


        public AccommodationRatingService(IAccommodationRatingRepository accommodationRatingRepository,
                                IReservationRepository reservationRepository)
        {
            _accommodationRatingRepository = accommodationRatingRepository;
            _reservationRepository = reservationRepository;
        }

        public bool CreateOrEditAccommodationRating(AccommodationRating accommRating)
        {
            if (EditAccommodationGradeIfExist(accommRating))
                return true;
            if (!CheckIfCanRate(accommRating.GuestId, accommRating.AccommodationId))
                return false;
            _accommodationRatingRepository.Create(accommRating);
            return true;
        }

        public void DeleteAccommodationRating(Guid guestId, Guid accommodationRatingId)
        {
            var rating = _accommodationRatingRepository.GetByGuestAndAccommodation(guestId, accommodationRatingId);
            _accommodationRatingRepository.Delete(rating.Id);
        }

        public List<AccommodationRating> GetAllByAccommodation(Guid accommodationRatingId)
        {
            return _accommodationRatingRepository.GetAllByAccommodation(accommodationRatingId);
        }

        public double GetAverageByAccommodation(Guid accommodationId)
        {
            return _accommodationRatingRepository.GetAverageGradeByAccommodation(accommodationId);
        }

        private bool CheckIfCanRate(Guid guestId, Guid accommodationId)
        {
            var res = _reservationRepository.GetReservationByGuestAndAccommodationInPast(guestId, accommodationId);
            if (res != null)
                return true;
            return false;
        }
        
        private bool EditAccommodationGradeIfExist(AccommodationRating accomRating)
        {
            var rating = _accommodationRatingRepository.GetByGuestAndAccommodation(accomRating.GuestId, accomRating.AccommodationId);
            if (rating != null)
            {
                rating.Grade = accomRating.Grade;
                rating.DateTimeOfGrade = DateTime.Now;
                _accommodationRatingRepository.Update(rating);
                return true;
            }
            return false;
        }
    }
}
