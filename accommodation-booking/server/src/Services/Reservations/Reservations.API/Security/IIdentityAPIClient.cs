namespace Reservations.API.Security
{
    public interface IIdentityAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
