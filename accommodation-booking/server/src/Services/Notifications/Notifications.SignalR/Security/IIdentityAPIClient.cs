namespace Notifications.SignalR.Security
{
    public interface IIdentityAPIClient
    {
        public bool ValidateAuthorizationHeader(string authorizationHeader);
    }
}
