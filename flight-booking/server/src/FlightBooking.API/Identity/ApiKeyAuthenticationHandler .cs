using FlightBooking.Business.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FlightBooking.API.Identity
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string ApiKeyHeaderName = "API-KEY";

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                return AuthenticateResult.Fail("API Key was not provided");
            }

            var apiKeyRepository = Context.RequestServices.GetRequiredService<IApiKeyRepository>();
            var apiKey = await apiKeyRepository.GetByKey(Guid.Parse(potentialApiKey));

            if(apiKey is null || apiKey.IsExpired())
                return AuthenticateResult.Fail("API key not valid");

            var claims = new List<Claim>
            {
                new Claim("UserId", apiKey.UserId)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
