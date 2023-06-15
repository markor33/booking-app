using Notifications.SignalR.Models;

namespace Notifications.SignalR.Services
{
    public interface INotificationService
    {
        Task SendNotification(Notification notification);
    }
}
