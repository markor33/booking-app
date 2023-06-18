namespace Web.Bff.Security
{
    public interface IIdentityAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
