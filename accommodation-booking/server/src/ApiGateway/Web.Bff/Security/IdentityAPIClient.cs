using RestSharp;

namespace Web.Bff.Security
{
    public class IdentityAPIClient : IIdentityAPIClient
    {
        private readonly RestClient _restClient;

        public IdentityAPIClient()
        {
            _restClient = new RestClient("http://host.docker.internal:11000/api/auth/validate");
        }

        public bool ValidateAuthorizationHeader(string authorizationHeader)
        {
            var request = new RestRequest("/auth/validate");
            request.AddHeader("Authorization", authorizationHeader);
            try
            {
                _restClient.Get(request);
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
