using Grpc.Core;
using GrpcReservations;
using ReservationsLibrary.Services;

namespace Reservations.API.GrpcServices
{
    public class ReservationsGrpcService : GrpcReservations.Reservations.ReservationsBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsGrpcService(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async override Task<CheckActiveReservationsResponse> CheckActiveReservations(CheckActiveReservationsRequest request, ServerCallContext context)
        {
            bool result;
            if (request.Role == "HOST")
                result = _reservationService.ActiveHostReservations(Guid.Parse(request.UserId));
            else
                result = _reservationService.ActiveGuestReservations(Guid.Parse(request.UserId));
            return new CheckActiveReservationsResponse()
            {
                HasActive = result
            };
        }
    }
}
