using Microsoft.EntityFrameworkCore;
using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure.Persistence.Repositories
{
    public class AccommodationRepository : EntityRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(ReservationsDbContext dbContext) : base(dbContext) { }

        public async Task<bool> DeleteAccommodationByHost(Guid hostId)
        {
            var accommodations = _dbContext
                .Accommodations
                .Include(a => a.ReservationRequests)
                .Include(a => a.Reservations)
                .Where(a => a.HostId == hostId);

            foreach (var accommodation in accommodations)
                accommodation.SetDelete(true);
            
            return await _dbContext.SaveEntitiesAsync();
        }

        public async Task<List<Accommodation>> GetByHost(Guid hostId)
        {
            return await _dbContext
                .Accommodations
                .Include(a => a.ReservationRequests)
                .Include(a => a.Reservations)
                .Where(a => a.HostId == hostId).ToListAsync();
        }
    }
}
