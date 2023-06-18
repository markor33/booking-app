using Grpc.Core;
using GrpcIdentity;
using Identity.API.Services;

namespace Identity.API.GrpcService
{
    public class IdentityGrpcService : GrpcIdentity.Identity.IdentityBase
    {
        private readonly ApplicationUserService _applicationUserService;

        public IdentityGrpcService(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async override Task<GetGuestFullNameResponse> GetGuestFullName(GetGuestFullNameRequest request, ServerCallContext context)
        {
            var fullName = await _applicationUserService.GetUserFullName(request.GuestId);
            var response = new GetGuestFullNameResponse { GuestFullName = fullName.Value };
            return response;
        }

    }
}
