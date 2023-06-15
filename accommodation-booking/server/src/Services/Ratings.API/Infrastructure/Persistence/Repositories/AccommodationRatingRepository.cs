using Microsoft.Extensions.Hosting;
using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class AccommodationRatingRepository : EntityRepository<AccommodationRating>, IAccommodationRatingRepository
    {
        public AccommodationRatingRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public List<AccommodationRating> GetAllByAccommodation(Guid accommodationId)
        {
            return _dbContext.AccommodationRatings.Where(rating => rating.AccommodationId == accommodationId).ToList();
        }

        public double GetAverageGradeByAccommodation(Guid accommodationId)
        {
            return _dbContext.AccommodationRatings.Where(rating => rating.AccommodationId == accommodationId)
                                                    .Average(rating => rating.Grade);
        }

        public AccommodationRating GetByGuestAndAccommodation(Guid guestId, Guid accommodationId)
        {
            return _dbContext.AccommodationRatings.FirstOrDefault(rating => rating.GuestId == guestId && rating.AccommodationId == accommodationId);
        }
    }
}
