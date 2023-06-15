using MongoDB.Driver;
using Notifications.SignalR.Models;
using Notifications.SignalR.Persistence.Settings;

namespace Notifications.SignalR.Persistence.Repositories
{
    public class UserNotificationsConfigRepository : IUserNotificationsConfigRepository
    {
        private readonly IMongoCollection<UserNotificationsConfig> _collection;

        public UserNotificationsConfigRepository(IMongoDbFactory mongoDb)
        {
            _collection = mongoDb.GetCollection<UserNotificationsConfig>("user-notifications-config");
        }

        public async Task<UserNotificationsConfig> GetByUser(Guid userId)
        {
            var filter = Builders<UserNotificationsConfig>.Filter.Eq(n => n.UserId, userId);
            var config = await (await _collection.FindAsync<UserNotificationsConfig>(filter)).FirstOrDefaultAsync();

            return config;
        }

        public async Task Create(UserNotificationsConfig config)
        {
            await _collection.InsertOneAsync(config);
        }

        public async Task Update(UserNotificationsConfig config)
        {
            var filter = Builders<UserNotificationsConfig>.Filter.Eq(n => n.Id, config.Id);
            await _collection.ReplaceOneAsync(filter, config);
        }
    }
}
