using MongoDB.Bson.Serialization.Attributes;

namespace Notifications.SignalR.Models
{
    public class UserNotificationsConfig
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Dictionary<string, bool> NotificationsConfig { get; set; }

        public UserNotificationsConfig(Guid userId, Dictionary<string, bool> notificationsConfig)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            NotificationsConfig = notificationsConfig;
        }
    }
}
