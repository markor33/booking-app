using GrpcAccommodationSearch;
using GrpcIdentity;
using GrpcRatings;
using GrpcReservations;
using Newtonsoft.Json;
using RestSharp;
using Web.Bff.Models;

namespace Web.Bff.Services
{
    public class ReservationService
    {
        private readonly Ratings.RatingsClient _ratingsClient;
        private readonly Reservations.ReservationsClient _reservationsClient;
        private readonly Identity.IdentityClient _identityClient;
        private RestClient _restClient;

        public ReservationService(Ratings.RatingsClient ratingsClient, Reservations.ReservationsClient reservationsClient, Identity.IdentityClient identityClient)
        {
            _ratingsClient = ratingsClient;
            _reservationsClient = reservationsClient;
            _restClient = new RestClient("http://host.docker.internal:9000/api/accomodation/cover/");
            _identityClient = identityClient;
        }

        public List<AggregateReservation> GetReservationsByUser(string userId, string userRole)
        {
            var reservations = _reservationsClient.GetReservationsForUser(new GetReservationsForUserRequest { UserId = userId, UserRole = userRole }).Reservations;
            var responseRatings = _ratingsClient.GetGradesForUser(new GradesRequest { UserRole = userRole, UserId = userId });
            var accomRatings = responseRatings.AccommGrades;
            var hostRatings = responseRatings.HostGrades;

            var list = new List<AggregateReservation>();

            for(int i = 0; i < reservations.Count; i++)
            {
                RestRequest request = new("{id}");
                request.AddUrlSegment("id", reservations[i].AccommodationId);
                RestResponse response = _restClient.Execute(request);
                AccomodationCardDTO ac = JsonConvert.DeserializeObject<AccomodationCardDTO>(response.Content);

                var aggReservation = new AggregateReservation
                {
                    AccommodationId = reservations[i].AccommodationId,
                    GuestId = reservations[i].GuestId,
                    Period = new Utils.DateRange { Start = DateTime.Parse(reservations[i].Period.StartDate), End = DateTime.Parse(reservations[i].Period.EndDate) },
                    NumOfGuests = reservations[i].NumOfGuests,
                    Price = reservations[i].Price,
                    Canceled = reservations[i].Canceled,
                    HRating = hostRatings[i],
                    ARating = accomRatings[i],
                    UserFullName = _identityClient.GetGuestFullName(new GetGuestFullNameRequest { GuestId = reservations[i].GuestId }).GuestFullName,
                    AccommName = ac.Name,
                    AccommPhoto = ac.PhotoUrl,
                    Id = reservations[i].Id
                };
                list.Add(aggReservation);
            }

            return list;
        }
  
    }
}
