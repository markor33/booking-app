using Notifications.SignalR.Persistence.Repositories;
using System.Collections.Concurrent;

namespace Notifications.SignalR.Services
{
    public class ActiveUserService : IActiveUserService
    {
        private ConcurrentDictionary<Guid, string> _activeUsers;

        public ActiveUserService()
        {
            _activeUsers = new ConcurrentDictionary<Guid, string>();
        }

        public string GetConnectionId(Guid userId)
        {
            _activeUsers.TryGetValue(userId, out var connectionId);
            return connectionId;
        }

        public async void UserConnected(string connectionId, Guid userId)
        {
            _activeUsers.TryAdd(userId, connectionId);
        }

        public void UserDisconnected(Guid userId)
        {
            _activeUsers.TryRemove(userId, out string connectionId);
        }
    }
}
