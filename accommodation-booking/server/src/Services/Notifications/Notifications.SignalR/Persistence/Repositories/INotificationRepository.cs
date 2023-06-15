using Notifications.SignalR.Models;

namespace Notifications.SignalR.Persistence.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetByUser(Guid userId);
        Task Create(Notification notification);
    }
}
