using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;

namespace RatingsLibrary.Repository
{
    public interface IHostRatingRepository : IEntityRepository<HostRating>
    {
        HostRating GetByGuestAndHost(Guid guestId, Guid hostId);
        List<HostRating> GetAllByHost(Guid hostId);
        double GetAverageGradeByHost(Guid hostId);
    }
}
