using Notifications.SignalR.Models;

namespace Notifications.SignalR.Persistence.Repositories
{
    public interface IUserNotificationsConfigRepository
    {
        Task<UserNotificationsConfig> GetByUser(Guid userId);
        Task Create(UserNotificationsConfig config);
        Task Update(UserNotificationsConfig config);
    }
}
