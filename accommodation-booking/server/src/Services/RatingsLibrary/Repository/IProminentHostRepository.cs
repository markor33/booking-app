using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;

namespace RatingsLibrary.Repository
{
    public interface IProminentHostRepository : IEntityRepository<ProminentHost>
    {
        ProminentHost GetByHost(Guid hostId);
    }
}
