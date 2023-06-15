using MongoDB.Driver;

namespace Notifications.SignalR.Persistence.Settings
{
    public interface IMongoDbFactory
    {
        public IMongoCollection<T> GetCollection<T>();
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
