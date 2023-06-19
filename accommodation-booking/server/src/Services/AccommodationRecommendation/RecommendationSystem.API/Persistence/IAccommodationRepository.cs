using RecommendationSystem.API.DTO;
using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public interface IAccommodationRepository
    {
        Task<List<RecommendedAccommodationDTO>> GetRecommended(Guid userId);
        Task Create(Accommodation accommodation);
        Task Delete(Guid id);
        Task<bool> SetDeleteByHost(Guid hostId, bool isDeleted);
    }
}
