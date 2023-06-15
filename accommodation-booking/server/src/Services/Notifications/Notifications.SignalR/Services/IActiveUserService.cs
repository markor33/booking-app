namespace Notifications.SignalR.Services
{
    public interface IActiveUserService
    {
        void UserConnected(string connectionId, Guid userId);
        void UserDisconnected(Guid userId);
        string GetConnectionId(Guid userId);

    }
}
