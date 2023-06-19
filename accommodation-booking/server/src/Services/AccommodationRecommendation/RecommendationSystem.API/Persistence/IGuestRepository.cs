using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public interface IGuestRepository
    {
        Task Create(Guest guest);
    }
}
