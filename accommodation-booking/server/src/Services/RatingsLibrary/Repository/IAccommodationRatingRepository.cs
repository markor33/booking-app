using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;

namespace RatingsLibrary.Repository
{
    public interface IAccommodationRatingRepository : IEntityRepository<AccommodationRating>
    {
        AccommodationRating GetByGuestAndAccommodation(Guid guestId, Guid accommodationId);
        AccommodationRating GetByReservationId(Guid reservationId);
        List<AccommodationRating> GetAllByAccommodation(Guid accommodationId);
        double GetAverageGradeByAccommodation(Guid accommodationId);
        List<int> GetGradesByGuest(Guid guestId);
        List<int> GetGradesByHost(Guid hostId);
    }
}
