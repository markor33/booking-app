using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories.Base;

namespace FlightBooking.Business.Repositories
{
    public interface IApiKeyRepository : IRepository<ApiKey>
    {
        Task<ApiKey> GetByUser(string userId);
        Task<ApiKey> GetByKey(Guid key);
    }
}
