using MongoDB.Driver;
using Notifications.SignalR.Models;
using Notifications.SignalR.Persistence.Settings;

namespace Notifications.SignalR.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public NotificationRepository(IMongoDbFactory mongoDb)
        {
            _notificationsCollection = mongoDb.GetCollection<Notification>("notifications");
        }

        public async Task<List<Notification>> GetByUser(Guid userId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId);
            var notifications = await (await _notificationsCollection.FindAsync<Notification>(filter)).ToListAsync();
            
            return notifications;
        }

        public async Task Create(Notification notification)
        {
            await _notificationsCollection.InsertOneAsync(notification);
        }

    }
}
