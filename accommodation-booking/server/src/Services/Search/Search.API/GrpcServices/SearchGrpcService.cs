using Grpc.Core;
using GrpcAccommodationSearch;
using Search.API.DTO;
using Search.API.Services;
using AccommodationDTO = Search.API.DTO.AccommodationDTO;
using GrpcAccommodationDTO = GrpcAccommodationSearch.AccommodationDTO;

namespace Search.API.GrpcServices
{
    public class SearchGrpcService : AccommodationSearch.AccommodationSearchBase
    {
        private readonly ISearchService _searchService;

        public SearchGrpcService(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public override async Task<SearchResponse> SearchAccommodations(SearchRequest request, ServerCallContext context)
        {
            SearchArgs args = new()
            {
                Location = request.Location,
                NumOfGuests = request.NumOfGuests,
                Start = DateTime.Parse(request.Start),
                End = DateTime.Parse(request.End)
            };

            List<GrpcAccommodationDTO> searchResults = new();

            List<AccommodationDTO> foundAccommodations = await _searchService.SearchAccommodations(args);

            foreach (AccommodationDTO accommodation in foundAccommodations)
            {
                GrpcAccommodationDTO searchResult = new()
                {
                    Id = accommodation.Id.ToString(),
                    HostId = accommodation.HostId.ToString(),
                    Name = accommodation.Name,
                    Description = accommodation.Description,
                    Location = new Address
                    {
                        Country = accommodation.Location.Country,
                        City = accommodation.Location.City,
                        Street = accommodation.Location.Street,
                        Number = accommodation.Location.Number
                    },
                    MinGuests = accommodation.MinGuests,
                    MaxGuests = accommodation.MaxGuests,
                    Photo = accommodation.Photo,
                    Benefits =
                    {
                        accommodation.Benefits.Select(benefit => new Benefit
                        {
                            Id = benefit.Id.ToString(),
                            Name = benefit.Name
                        })
                    },
                    PriceType = (PriceType)accommodation.PriceType,
                    Price = accommodation.Price
                };

                searchResults.Add(searchResult);
            }

            SearchResponse response = new();
            response.Accommodations.AddRange(searchResults);

            return response;
        }
    }
}
