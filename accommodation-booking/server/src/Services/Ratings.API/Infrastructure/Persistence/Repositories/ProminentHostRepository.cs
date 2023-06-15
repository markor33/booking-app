using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class ProminentHostRepository : EntityRepository<ProminentHost>, IProminentHostRepository
    {
        public ProminentHostRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public ProminentHost GetByHost(Guid hostId)
        {
            return _dbContext.ProminentHosts.FirstOrDefault(host => host.HostId == hostId);
        }
    }
}
