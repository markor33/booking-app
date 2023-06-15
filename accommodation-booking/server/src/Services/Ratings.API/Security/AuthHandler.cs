using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Ratings.API.Security
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IIdentityAPIClient _identityAPIClient;

        public AuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IIdentityAPIClient hospitalAPIClient) : base(options, logger, encoder, clock)
        {
            _identityAPIClient = hospitalAPIClient;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.Authorization.Count == 0)
                return AuthenticateResult.NoResult();
            var authorizationHeader = Request.Headers.Authorization.ToString();
            if (!_identityAPIClient.ValidateAuthorizationHeader(authorizationHeader))
                return AuthenticateResult.Fail("Token is not valid");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(authorizationHeader.Replace("Bearer ", ""));
            var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);

            var roleClaimValue = jwt.Claims.First(c => c.Type == "role").Value;
            var idClaimValue = jwt.Claims.First(c => c.Type == "sub").Value;
            var emailClaimValue = jwt.Claims.First(c => c.Type == "email").Value;

            identity.AddClaim(new Claim(ClaimTypes.Role, roleClaimValue));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, idClaimValue));
            identity.AddClaim(new Claim(ClaimTypes.Email, emailClaimValue));

            var principial = new ClaimsPrincipal(identity);
            var authTicket = new AuthenticationTicket(principial, Scheme.Name);
            return AuthenticateResult.Success(authTicket);
        }

    }
}
