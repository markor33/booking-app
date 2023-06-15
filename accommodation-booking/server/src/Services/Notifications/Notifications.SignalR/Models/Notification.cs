using MongoDB.Bson.Serialization.Attributes;

namespace Notifications.SignalR.Models
{
    public class Notification
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Text { get; private set; }
        public string Type { get; private set; }

        public Notification(Guid userId, string text, string type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Text = text;
            Type = type;
        }
    }
}
