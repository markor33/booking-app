using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;
using System.Linq;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class ProminentHostRepository : EntityRepository<ProminentHost>, IProminentHostRepository
    {
        public ProminentHostRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProminentHost> CreateAsync(ProminentHost prominentHost)
        {
            await _dbContext.ProminentHosts.AddAsync(prominentHost);
            await _dbContext.SaveChangesAsync();
            return prominentHost;
        }
        public ProminentHost GetByHost(Guid hostId)
        {
            return  _dbContext.ProminentHosts.FirstOrDefault(host => host.HostId == hostId);
        }

        public async Task SetDurationAcceptable()
        {
            var hostsToCheck = _dbContext.ProminentHosts
                .Where(h => h.IsDurationOfReservationsAcceptable == false)
                .Select(r => r.HostId);


            var hostIdsToPromote = _dbContext.Reservations
                .Where(r => r.Period.End < DateTime.Now && hostsToCheck.Contains(r.HostId) && r.Canceled == false)
                .GroupBy(r => r.HostId)
                .Where(g => g.Sum(r => (int)(r.Period.End - r.Period.Start).TotalDays) > 50)
                .Select(g => g.Key);


            var hostToUpdateDurationAcceptable = await _dbContext.ProminentHosts
                .Where(h => hostIdsToPromote.Contains(h.HostId) && hostsToCheck.Contains(h.HostId))
                .ToListAsync();

            hostToUpdateDurationAcceptable.ForEach(h => h.IsDurationOfReservationsAcceptable = true);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetHasFiveReservationAcceptable()
        {
            var hostsToCheck = _dbContext.ProminentHosts
                .Where(h => h.HasFiveReservations == false)
                .Select(r => r.HostId);

            var hostsWithMoreThanFiveReservations = _dbContext.Reservations
                .Where(res => hostsToCheck.Contains(res.HostId) && res.Canceled == false && res.Period.End < DateTime.Now)
                .GroupBy(res => res.HostId)
                .Where(g => g.Count() > 5)
                .Select(g => g.Key);

            var hostsToUpdateHasFiveResAcceptable = await _dbContext.ProminentHosts
                .Where(h => hostsWithMoreThanFiveReservations.Contains(h.HostId) && hostsToCheck.Contains(h.HostId))
                .ToListAsync();

            hostsToUpdateHasFiveResAcceptable.ForEach(h => h.HasFiveReservations = true);
            await _dbContext.SaveChangesAsync();
        }
    }
}
