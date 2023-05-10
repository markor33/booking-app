using Grpc.Core;
using GrpcReservations;
using ReservationsLibrary.Services;
using System.Runtime.CompilerServices;

namespace Reservations.API.GrpcServices
{
    public class ReservationsGrpcService : GrpcReservations.Reservations.ReservationsBase
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationRequestService _reservationRequestService;

        public ReservationsGrpcService(IReservationService reservationService, IReservationRequestService reservationRequestService)
        {
            _reservationService = reservationService;
            _reservationRequestService = reservationRequestService;
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
        public async override Task<DeleteRequestAndReservationsResponse> DeleteRequestAndReservations(DeleteRequestAndReservationsRequest request, ServerCallContext context)
        {
            _reservationRequestService.DeleteAllRequestsByGuest(Guid.Parse(request.GuestId));
            _reservationService.DeleteAllReservationsByGuest(Guid.Parse(request.GuestId));
            return new DeleteRequestAndReservationsResponse();
        }
    }
}
