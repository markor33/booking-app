namespace Ratings.API.Security
{
    public interface IIdentityAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
