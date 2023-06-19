using FlightBooking.Business.Entities;


namespace FlightBooking.Business.Services
{
    public interface IApiKeyService
    {
        Task<ApiKey> GetByUser(string userId);
        Task<ApiKey> Create(string userId, bool isPermanent);
        Task<ApiKey> Update(string id, bool isPermanent);
        Task<ApiKey> RefreshExpireDate(string id);
        Task Delete(string id);
    }
}
