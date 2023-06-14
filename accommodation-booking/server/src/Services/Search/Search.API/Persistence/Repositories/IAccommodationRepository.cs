using Search.API.DTO;
using Search.API.Models;

namespace Search.API.Persistence.Repositories
{
    public interface IAccommodationRepository
    {
        Task<Accommodation> GetByIdAsync(Guid id);
        Task<List<Accommodation>> Search(SearchArgs searchArgs);
        Task CreateAsync(Accommodation accommodation);
        Task UpdateAsync(Accommodation accommodation);
        Task<bool> DeleteByHostAsync(Guid hostId);
        Task<bool> DeleteGuestReservations(Guid guestId);
    }
}
