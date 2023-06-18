using FlightBooking.Business.Entities;
using FlightBooking.Business.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlightBooking.Business.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<ApiKey> GetByUser(string userId)
            => await _apiKeyRepository.GetByUser(userId);

        public async Task<ApiKey> Create(string userId, bool isPermanent)
        {
            if (await HasAlreadyApiKey(userId))
                return null;

            var apiKey = new ApiKey(userId, isPermanent);
            return await _apiKeyRepository.CreateAsync(apiKey);
        }

        public async Task<ApiKey> Update(string id, bool isPermanent)
        {
            var apiKey = await _apiKeyRepository.GetByIdAsync(id);
            apiKey.SetPermanent(isPermanent);

            await _apiKeyRepository.UpdateAsync(apiKey);

            return apiKey;
        }

        public async Task Delete(string id)
            => await _apiKeyRepository.DeleteAsync(id);

        public async Task<ApiKey> RefreshExpireDate(string id)
        {
            var apiKey = await _apiKeyRepository.GetByIdAsync(id);
            apiKey.RefreshToken();

            await _apiKeyRepository.UpdateAsync(apiKey);

            return apiKey;
        }

        private async Task<bool> HasAlreadyApiKey(string userId)
        {
            var apiKey = await _apiKeyRepository.GetByUser(userId);
            if (apiKey is not null)
                return true;
            else 
                return false;
        }

    }
}
