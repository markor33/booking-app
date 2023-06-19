using GrpcAccommodationSearch;
using GrpcIdentity;
using GrpcRatings;
using Newtonsoft.Json;
using RestSharp;
using Web.Bff.Models;
using Address = Web.Bff.Models.Address;
using Benefit = Web.Bff.Models.Benefit;
using GrpcBenefit = GrpcAccommodationSearch.Benefit;
using PriceType = Web.Bff.Models.PriceType;

namespace Web.Bff.Services
{
    public class AccommodationService
    {
        private readonly AccommodationSearch.AccommodationSearchClient _searchClient;
        private readonly Ratings.RatingsClient _ratingsClient;
        private readonly Identity.IdentityClient _identityClient;
        private RestClient _restClient;

        public AccommodationService(AccommodationSearch.AccommodationSearchClient searchClient, Ratings.RatingsClient ratingsClient, Identity.IdentityClient identityClient)
        {
            _searchClient = searchClient;
            _ratingsClient = ratingsClient;
            _restClient = new RestClient("http://host.docker.internal:9000/api/accomodation/");
            _identityClient = identityClient;
        }

        public List<SearchExtended> SearchAccommodations(SearchArgs args)
        {
            var res = new List<SearchExtended>();
            var benefits = new List<GrpcBenefit>();
            foreach(var ben in args.Benefits)
            {
                benefits.Add(new GrpcBenefit { Id = ben.Id, Name = ben.Name });
            }
            var searchRequest = new SearchRequest
            {
                Location = args.Location,
                NumOfGuests = args.NumOfGuests,
                End = args.End.ToString(),
                Start = args.Start.ToString(),
                PriceRange = new GrpcAccommodationSearch.PriceRange { MaxPrice = args.PriceRange.MaxPrice, MinPrice = args.PriceRange.MinPrice }

            };
            searchRequest.Benefits.AddRange(benefits);
            var searchResponse = _searchClient.SearchAccommodations(searchRequest);

            
            
            foreach(AccommodationDTO accommDto in searchResponse.Accommodations)
            {
                var prominentResponse =  _ratingsClient.GetHostProminentStats(new GetHostProminentStatsRequest { AccommId = accommDto.Id, HostId = accommDto.HostId});
                if(args.IsHostProminent == prominentResponse.IsHostProminent)
                {
                    List<Benefit> convertedBenefits = accommDto.Benefits.Select(b => new Benefit { Id = b.Id, Name = b.Name }).ToList();
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
            }

            return res;
        }
        public AccomodationDTO GetAccommodationDialog(string accommId, string hostId)
        {
            var accommRatings = new List<AccommodationRatingDTO>();
            var hostRatings = new List<HostRatingDTO>();

            var ratings = _ratingsClient.GetRatings(new GetRatingsRequest { AccommId = accommId, HostId = hostId });
            var prominentStats = _ratingsClient.GetHostProminentStats(new GetHostProminentStatsRequest { AccommId = accommId, HostId = hostId });

            RestRequest request = new("{id}");
            request.AddUrlSegment("id", accommId);
            RestResponse response = _restClient.Execute(request);
            AccomodationDTO ac = JsonConvert.DeserializeObject<AccomodationDTO>(response.Content);

            foreach(var accommRating in ratings.AccommodationRatings){
                var accommRatingDTO = new AccommodationRatingDTO
                {
                    DateTimeOfGrade = DateTime.Parse(accommRating.DateTimeOfGrade),
                    Grade = accommRating.Grade,
                    GuestFullName = _identityClient.GetGuestFullName(new GetGuestFullNameRequest { GuestId = accommRating.GuestId }).GuestFullName
                };
                accommRatings.Add(accommRatingDTO);
            }

            foreach (var hostRating in ratings.HostRatings)
            {
                var hostRatingDTO = new HostRatingDTO
                {
                    DateTimeOfGrade = DateTime.Parse(hostRating.DateTimeOfGrade),
                    Grade = hostRating.Grade,
                    GuestFullName = _identityClient.GetGuestFullName(new GetGuestFullNameRequest { GuestId = hostRating.GuestId }).GuestFullName
                };
                hostRatings.Add(hostRatingDTO);
            }
            ac.AvgHostGrade = prominentStats.AvgHostGrade;
            ac.AvgAccommGrade = prominentStats.AvgAccommGrade;
            ac.IsHostProminent = prominentStats.IsHostProminent;
            ac.AccommRatings = accommRatings;
            ac.HostRatings = hostRatings;

            return ac;
        }


    }
}
