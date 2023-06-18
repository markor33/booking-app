using RatingsLibrary.Models;

namespace RatingsLibrary.Services
{
    public interface IHostRatingService
    {
        bool CreateOrEditHostRating(HostRating hostRating);
        void DeleteHostRating(Guid guestId,Guid hostId);
        List<HostRating> GetAllByHost(Guid hostId);
        double GetAverageByHost(Guid hostId);
        bool CheckIfCanRate(Guid guestId, Guid hostId);
        List<int> GetAllGradesByGuest(Guid guestId);
    }
}
