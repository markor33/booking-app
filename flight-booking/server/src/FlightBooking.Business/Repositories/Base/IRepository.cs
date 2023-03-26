using FlightBooking.Business.Entities.Base;

namespace FlightBooking.Business.Repositories.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(string id);
        public Task<T> CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(string id);
    }
}
