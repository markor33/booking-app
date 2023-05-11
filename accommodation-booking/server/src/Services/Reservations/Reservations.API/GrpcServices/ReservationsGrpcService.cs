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
        private readonly IAccommodationService _accommodationService;

        public ReservationsGrpcService(IReservationService reservationService,
                                       IReservationRequestService reservationRequestService,
                                       IAccommodationService accommodationService)
        {
            _reservationService = reservationService;
            _reservationRequestService = reservationRequestService;
            _accommodationService = accommodationService;
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
        public async override Task<DeleteUserDependenciesResponse> DeleteUserDependencies(DeleteUserDependenciesRequest request, ServerCallContext context)
        {
            if(request.Role == "HOST")
            {
                _reservationService.DeleteReservationsByHost(Guid.Parse(request.UserId));
                _reservationRequestService.DeleteReservationRequestsByHost(Guid.Parse(request.UserId));
                _accommodationService.DeleteAccommodationByHost(Guid.Parse(request.UserId));
            }
            _reservationService.DeleteAllReservationsByGuest(Guid.Parse(request.UserId));
            _reservationRequestService.DeleteAllRequestsByGuest(Guid.Parse(request.UserId));

            return new DeleteUserDependenciesResponse();
        }
    }
}
