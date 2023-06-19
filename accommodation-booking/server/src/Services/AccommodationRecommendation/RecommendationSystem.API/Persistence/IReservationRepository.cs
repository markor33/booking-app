using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public interface IReservationRepository
    {
        Task Create(Reservation reservation);
        Task Delete(Reservation reservation);
    }
}
