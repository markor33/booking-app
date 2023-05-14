using Grpc.Core;
using GrpcReservations;
using ReservationsLibrary.Models;
using ReservationsLibrary.Services;
using ReservationsLibrary.Utils;

namespace Reservations.API.Infrastructure.GrpcServices
{
    public class ReservationsGrpcService : GrpcReservations.Reservations.ReservationsBase
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IReservationService _reservationService;
        private readonly IReservationRequestService _reservationRequestService;

        public ReservationsGrpcService(IReservationService reservationService,
                                       IReservationRequestService reservationRequestService,
                                       IAccommodationService accommodationService)
        {
            _reservationService = reservationService;
            _accommodationService = accommodationService;
            _reservationRequestService = reservationRequestService;
            _accommodationService = accommodationService;
        }

        public override async Task<AddAccommodationResponse> AddAccommodation(AddAccommodationRequest request, ServerCallContext context)
        {
            var accommodation = new Accommodation()
            {
                Id = Guid.Parse(request.AccommodationId),
                HostId = Guid.Parse(request.HostId),
                AutoConfirmation = request.AutoConfirmation,
            };
            _accommodationService.Create(accommodation);
            return new AddAccommodationResponse();
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
            if (request.Role == "HOST")
            {
                _reservationService.DeleteReservationsByHost(Guid.Parse(request.UserId));
                _reservationRequestService.DeleteReservationRequestsByHost(Guid.Parse(request.UserId));
                _accommodationService.DeleteAccommodationByHost(Guid.Parse(request.UserId));
            }
            _reservationService.DeleteAllReservationsByGuest(Guid.Parse(request.UserId));
            _reservationRequestService.DeleteAllRequestsByGuest(Guid.Parse(request.UserId));

            return new DeleteUserDependenciesResponse();
        }

        public async override Task<IsOverLappedByAccomodationResponse> IsOverLappedByAccomodation(IsOverLappedByAccomodationRequest request, ServerCallContext context)
        {
            var accommodationId = Guid.Parse(request.AccommodationId);
            var range = new DateRange(request.StartDate.ToDateTime(), request.EndDate.ToDateTime());
            var result = _reservationService.IsOverLappedByAccomodation(range, accommodationId);
            return new IsOverLappedByAccomodationResponse() { IsOverlapped = result };
        }
    }
}
