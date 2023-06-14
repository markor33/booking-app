using Search.API.DTO;
using Search.API.Persistence.Repositories;

namespace Search.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public SearchService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public async Task<List<AccommodationDTO>> SearchAccommodations(SearchArgs args)
        {
            var filters = args.FilterArgs;
            var accommodations = await _accommodationRepository.Search(args);

            var accommodationDTOs = new List<AccommodationDTO>();
            foreach (var accommodation in accommodations)
                accommodationDTOs.Add(new AccommodationDTO(accommodation, args.NumOfGuests, args.Start, args.End));

            if (filters.MinPrice is not null)
                accommodationDTOs = accommodationDTOs.Where(a => a.Price >= filters.MinPrice && a.Price <= filters.MaxPrice).ToList();

            return accommodationDTOs;
        }

    }
}
