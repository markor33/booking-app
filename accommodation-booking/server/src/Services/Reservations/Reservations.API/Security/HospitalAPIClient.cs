using RestSharp;

namespace Reservations.API.Security
{
    public class HospitalAPIClient : IHospitalAPIClient
    {
        private readonly RestClient _restClient;

        public HospitalAPIClient()
        {
            _restClient = new RestClient("http://host.docker.internal:10000/api/identity/auth/validate");
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
