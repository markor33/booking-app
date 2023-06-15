using Microsoft.AspNetCore.SignalR;
using Notifications.SignalR.Hubs;
using Notifications.SignalR.Models;
using Notifications.SignalR.Persistence.Repositories;

namespace Notifications.SignalR.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly IActiveUserService _activeUserService;
        private readonly IUserNotificationsConfigRepository _configRepository;

        public NotificationService(
            INotificationRepository notificationRepository, 
            IHubContext<NotificationsHub> hubContext, 
            IActiveUserService activeUserService,
            IUserNotificationsConfigRepository configRepository)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _activeUserService = activeUserService;
            _configRepository = configRepository;
        }

        public async Task SendNotification(Notification notification)
        {
            await _notificationRepository.Create(notification);

            var connectionId = _activeUserService.GetConnectionId(notification.UserId);
            if (connectionId is null)
                return;

            var isEnabled = await IsNotificationEnabled(notification.UserId, notification.Type);
            if (isEnabled)
                await _hubContext.Clients.Client(connectionId).SendAsync("newNotification", notification);
        }

        private async Task<bool> IsNotificationEnabled(Guid userId, string notificationName)
        {
            var config = await _configRepository.GetByUser(userId);
            if (config is null)
                return true;

            config.NotificationsConfig.TryGetValue(notificationName, out bool isEnabled);

            return isEnabled;
        }
    }
}
