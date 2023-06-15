using RatingsLibrary.Models;

namespace RatingsLibrary.Services
{
    public interface IHostRatingService
    {
        bool CreateOrEditHostRating(HostRating hostRating);
        void DeleteHostRating(Guid hostRatingId);
        List<HostRating> GetAllByHost(Guid hostId);
        double GetAverageByHost(Guid hostId);
    }
}
