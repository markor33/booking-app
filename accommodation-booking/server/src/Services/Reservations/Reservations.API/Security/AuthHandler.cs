using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Reservations.API.Security
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IHospitalAPIClient _hospitalAPIClient;

        public AuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHospitalAPIClient hospitalAPIClient) : base(options, logger, encoder, clock)
        {
            _hospitalAPIClient = hospitalAPIClient;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.Authorization.Count == 0)
                return AuthenticateResult.NoResult();
            var authorizationHeader = Request.Headers.Authorization.ToString();
            if (!_hospitalAPIClient.ValidateAuthorizationHeader(authorizationHeader))
                return AuthenticateResult.Fail("Token is not valid");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(authorizationHeader.Replace("Bearer ", ""));
            var identity = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, Scheme.Name));
            var authTicket = new AuthenticationTicket(identity, Scheme.Name);
            return AuthenticateResult.Success(authTicket);
        }

    }
}
