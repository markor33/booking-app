using RatingsLibrary.Models;

namespace RatingsLibrary.Services
{
    public interface IProminentHostService
    {
        void UpdateGradeAcceptableForHost(Guid hostId);
        void UpdateCancellationRateAcceptableForHost(Guid hostId);
        bool IsHostProminent(Guid hostId);
        ProminentHostStats GetHostProminentStats(Guid hostId, Guid accommId);
    }
}
