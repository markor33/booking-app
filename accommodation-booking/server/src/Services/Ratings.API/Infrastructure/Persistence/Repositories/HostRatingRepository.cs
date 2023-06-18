using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class HostRatingRepository : EntityRepository<HostRating>, IHostRatingRepository
    {
        public HostRatingRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public List<HostRating> GetAllByHost(Guid hostId)
        {
            return _dbContext.HostRatings.Where(rating => rating.HostId == hostId).ToList();
        }

        public List<int> GetAllGradesByGuest(Guid guestId)
        {
            return _dbContext.HostRatings.Where(rating => rating.GuestId == guestId).Select(x => x.Grade).ToList();
        }

        public double GetAverageGradeByHost(Guid hostId)
        {
            return _dbContext.HostRatings.Where(rating => rating.HostId == hostId)
                                                    .Average(rating => rating.Grade);
        }

        public HostRating GetByGuestAndHost(Guid guestId, Guid hostId)
        {
            return _dbContext.HostRatings.FirstOrDefault(rating => rating.GuestId == guestId && rating.HostId == hostId);
        }
    }
}
