namespace RecommendationSystem.API.Security.API.Security
{
    public interface IIdentityAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
