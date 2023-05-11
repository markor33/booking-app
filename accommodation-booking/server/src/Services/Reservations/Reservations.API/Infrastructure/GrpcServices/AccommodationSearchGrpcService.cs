using Google.Protobuf.WellKnownTypes;
using GrpcAccommodationSearch;
using ReservationsLibrary.Models;
using ReservationsLibrary.Services;

namespace Reservations.API.Infrastructure.GrpcServices
{
    public class AccommodationSearchGrpcService : IAccommodationSearchGrpcService
    {
        private readonly AccommodationSearch.AccommodationSearchClient _client;

        public AccommodationSearchGrpcService(AccommodationSearch.AccommodationSearchClient client)
        {
            _client = client;
        }

        public async Task AddReservation(Reservation reservation)
        {
            var request = new CreateReservationRequest()
            {
                Id = reservation.Id.ToString(),
                AccommodationId = reservation.AccommodationId.ToString(),
                StartDate = Timestamp.FromDateTimeOffset(reservation.Period.Start),
                EndDate = Timestamp.FromDateTimeOffset(reservation.Period.End)
            };
            await _client.CreateReservationAsync(request);
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            var request = new DeleteReservationRequest()
            {
                Id = reservation.Id.ToString(),
                AccommodationId = reservation.AccommodationId.ToString()
            };
            await _client.DeleteReservationAsync(request);
        }
    }
}
