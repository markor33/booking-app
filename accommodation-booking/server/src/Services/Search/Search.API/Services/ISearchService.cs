using Search.API.DTO;

namespace Search.API.Services
{
    public interface ISearchService
    {
        Task<List<AccommodationDTO>> SearchAccommodations(SearchArgs args);
    }
}
