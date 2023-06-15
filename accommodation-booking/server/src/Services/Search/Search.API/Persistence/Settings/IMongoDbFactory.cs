using MongoDB.Driver;

namespace Search.API.Persistence.Settings
{
    public interface IMongoDbFactory
    {
        public IMongoCollection<T> GetCollection<T>();
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
