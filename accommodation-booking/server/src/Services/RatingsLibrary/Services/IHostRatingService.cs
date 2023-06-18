using RatingsLibrary.Models;

namespace RatingsLibrary.Services
{
    public interface IHostRatingService
    {
        bool CreateOrEditHostRating(HostRating hostRating);
        void DeleteHostRating(Guid reservationId);
        List<HostRating> GetAllByHost(Guid hostId);
        double GetAverageByHost(Guid hostId);
        bool CheckIfCanRate(Guid guestId, Guid hostId);
        List<int> GetGradesByGuest(Guid guestId, string role);
    }
}
