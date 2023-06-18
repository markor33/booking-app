using GrpcAccommodationSearch;
using GrpcRatings;
using Search.API.DTO;
using Web.Bff.Models;
using Address = Web.Bff.Models.Address;
using Benefit = Web.Bff.Models.Benefit;
using PriceType = Web.Bff.Models.PriceType;

namespace Web.Bff.Services
{
    public class AggregationService
    {
        private readonly AccommodationSearch.AccommodationSearchClient _searchClient;
        private readonly Ratings.RatingsClient _ratingsClient;

        public AggregationService(AccommodationSearch.AccommodationSearchClient searchClient, Ratings.RatingsClient ratingsClient)
        {
            _searchClient = searchClient;
            _ratingsClient = ratingsClient;
        }

        public List<SearchExtended> SearchAccommodations(SearchArgs args)
        {
            var res = new List<SearchExtended>();
            var searchResponse = _searchClient.SearchAccommodations(new SearchRequest { Location = args.Location, NumOfGuests = args.NumOfGuests, End = args.End.ToString(), Start = args.Start.ToString()});
            
            foreach(AccommodationDTO accommDto in searchResponse.Accommodations)
            {
                var prominentResponse =  _ratingsClient.GetHostProminentStats(new GetHostProminentStatsRequest { AccommId = accommDto.Id, HostId = accommDto.HostId});
                List<Benefit> convertedBenefits = accommDto.Benefits.Select(b => new Benefit { Id = Guid.Parse(b.Id), Name = b.Name }).ToList();
                var searchItem = new SearchExtended
                {
                    Id = Guid.Parse(accommDto.Id),
                    HostId = Guid.Parse(accommDto.HostId),
                    Name = accommDto.Name,
                    Description = accommDto.Description,
                    Location = new Address(accommDto.Location.Country, accommDto.Location.City, accommDto.Location.Street, accommDto.Location.Number),
                    MinGuests = accommDto.MinGuests,
                    MaxGuests = accommDto.MaxGuests,
                    Photo = accommDto.Photo,
                    PriceType = (PriceType)accommDto.PriceType,
                    Price = accommDto.Price,
                    IsHostProminent = prominentResponse.IsHostProminent,
                    AvgHostGrade = prominentResponse.AvgHostGrade,
                    AvgAccommGrade = prominentResponse.AvgAccommGrade,
                    Benefits = convertedBenefits
                };
                res.Add(searchItem);
            }

            return res;
        }

    }
}
