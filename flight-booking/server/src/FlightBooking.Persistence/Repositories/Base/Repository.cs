using FlightBooking.Business.Entities.Base;
using FlightBooking.Business.Repositories.Base;
using FlightBooking.Persistence.Settings;
using MongoDB.Driver;

namespace FlightBooking.Persistence.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public Repository(IMongoDbFactory mongoDbFactory)
        {
            _collection= mongoDbFactory.GetCollection<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            return await (await _collection.FindAsync(filter)).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            return await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

    }
}
