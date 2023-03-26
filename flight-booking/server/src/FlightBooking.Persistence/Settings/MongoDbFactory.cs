using FlightBooking.Business.Helpers.CustomAttributes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace FlightBooking.Persistence.Settings
{
    public class MongoDbFactory : IMongoDbFactory
    {
        private readonly IMongoDatabase _database;

        public MongoDbFactory(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            _database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private static string GetCollectionName(Type t)
        {
            BsonCollectionAttribute? collectionAttribute = t.GetCustomAttribute(typeof(BsonCollectionAttribute)) as BsonCollectionAttribute;
            if (collectionAttribute == null)
                throw new Exception();
            return collectionAttribute.CollectionName;
        }
    }
}
