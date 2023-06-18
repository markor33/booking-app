using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;

namespace RatingsLibrary.Repository
{
    public interface IProminentHostRepository : IEntityRepository<ProminentHost>
    {
        Task<ProminentHost> CreateAsync(ProminentHost prominentHost);
        ProminentHost GetByHost(Guid hostId);
        Task SetDurationAcceptable();
        Task SetHasFiveReservationAcceptable();
    }
}
