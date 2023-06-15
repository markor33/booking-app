using RestSharp;

namespace Reservations.API.Security
{
    public class BookingAPIClient : IBookingAPIClient
    {
        private readonly RestClient _restClient;

        public BookingAPIClient()
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
