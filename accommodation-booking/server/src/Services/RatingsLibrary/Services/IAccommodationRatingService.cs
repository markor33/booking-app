using RatingsLibrary.Models;

namespace RatingsLibrary.Services
{
    public interface IAccommodationRatingService
    {
        bool CreateOrEditAccommodationRating(AccommodationRating accommodationRating);
        void DeleteAccommodationRating(Guid accommodationRatingId);
        List<AccommodationRating> GetAllByAccommodation(Guid accommodationRatingId);
        double GetAverageByAccommodation(Guid accommodationId);
    }
}
