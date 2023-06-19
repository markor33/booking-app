using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public interface IReviewRepository
    {
        Task Create(Review review);
        Task Update(Review review);
        Task Delete(Review review);
    }
}
