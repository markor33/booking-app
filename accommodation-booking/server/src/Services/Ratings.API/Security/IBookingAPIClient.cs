namespace Ratings.API.Security
{
    public interface IBookingAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
