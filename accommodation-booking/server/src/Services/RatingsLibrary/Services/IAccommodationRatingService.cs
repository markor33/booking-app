using RatingsLibrary.Models;

namespace RatingsLibrary.Services
{
    public interface IAccommodationRatingService
    {
        bool CreateOrEditAccommodationRating(AccommodationRating accommodationRating);
        void DeleteAccommodationRating(Guid reservationId);
        List<AccommodationRating> GetAllByAccommodation(Guid accommodationRatingId);
        double GetAverageByAccommodation(Guid accommodationId);
        List<int> GetGradesByGuest(Guid guestId, string role);
    }
}
