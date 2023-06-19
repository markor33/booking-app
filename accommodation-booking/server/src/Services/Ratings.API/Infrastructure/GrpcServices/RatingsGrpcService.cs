using Grpc.Core;
using GrpcRatings;
using RatingsLibrary.Services;
using GrpcHostRating = GrpcRatings.HostRating;
using HostRating = RatingsLibrary.Models.HostRating;
using GrpcAccommodationRating = GrpcRatings.AccommodationRating;
using AccommodationRating = RatingsLibrary.Models.AccommodationRating;

namespace Ratings.API.Infrastructure.GrpcServices
{
    public class RatingsGrpcService : GrpcRatings.Ratings.RatingsBase
    {
        private readonly IProminentHostService _prominentHostService;
        private readonly IHostRatingService _hostRatingService;
        private readonly IAccommodationRatingService _accommodationRatingService;

        public RatingsGrpcService(IProminentHostService prominentHostService, IHostRatingService hostRatingService, IAccommodationRatingService accommodationRatingService)
        {
            _prominentHostService = prominentHostService;
            _hostRatingService = hostRatingService;
            _accommodationRatingService = accommodationRatingService;
        }
        public override Task<ProminentHostStats> GetHostProminentStats(GetHostProminentStatsRequest request, ServerCallContext context)
        {
            var stats = _prominentHostService.GetHostProminentStats(Guid.Parse(request.HostId), Guid.Parse(request.AccommId));


            return Task.FromResult(new ProminentHostStats
            {
                IsHostProminent = stats.IsHostProminent,
                AvgAccommGrade = (float)stats.AvgAccommGrade,
                AvgHostGrade = (float)stats.AvgHostGrade
            });
        }

        public override Task<GetRatingsResponse> GetRatings(GetRatingsRequest request, ServerCallContext context)
        {
            var hostRatings = _hostRatingService.GetAllByHost(Guid.Parse(request.HostId));
            var accomRatings = _accommodationRatingService.GetAllByAccommodation(Guid.Parse(request.AccommId));
            var avgHostGrade = _hostRatingService.GetAverageByHost(Guid.Parse(request.HostId));
            var avgAccommGrade = _accommodationRatingService.GetAverageByAccommodation(Guid.Parse(request.HostId));

            var response = new GetRatingsResponse
            {
                AvgAccomGrade = (float)avgAccommGrade,
                AvgHostGrade = (float)avgHostGrade
            };

            var tempHosts = new List<GrpcHostRating>();
            var tempAccomms = new List<GrpcAccommodationRating>();

            foreach(HostRating hostRating in hostRatings)
            {
                GrpcHostRating grpcRating = new()
                {
                    Grade = hostRating.Grade,
                    GuestId = hostRating.GuestId.ToString(),
                    DateTimeOfGrade = hostRating.DateTimeOfGrade.ToString()
                };

                tempHosts.Add(grpcRating);  
            }

            foreach (AccommodationRating accommRating in accomRatings)
            {
                GrpcAccommodationRating grpcRating = new()
                {
                    Grade = accommRating.Grade,
                    GuestId = accommRating.GuestId.ToString(),
                    DateTimeOfGrade = accommRating.DateTimeOfGrade.ToString()
                };

                tempAccomms.Add(grpcRating);
            }

            response.AccommodationRatings.AddRange(tempAccomms);
            response.HostRatings.AddRange(tempHosts);

            return Task.FromResult(response);
        }

        public override Task<GradesResponse> GetGradesForUser(GradesRequest request, ServerCallContext context)
        {
            var gradeResponse = new GradesResponse();
            var userId = Guid.Parse(request.UserId);
            var userRole = request.UserRole;
            gradeResponse.AccommGrades.AddRange(_accommodationRatingService.GetGradesByGuest(userId, userRole));
            gradeResponse.HostGrades.AddRange(_hostRatingService.GetGradesByGuest(userId, userRole));

            return Task.FromResult(gradeResponse);
        }
    }
}
