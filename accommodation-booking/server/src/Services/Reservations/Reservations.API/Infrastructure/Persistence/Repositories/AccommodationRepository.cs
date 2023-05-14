using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure.Persistence.Repositories
{
    public class AccommodationRepository : EntityRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(ReservationsDbContext dbContext) : base(dbContext) { }

        public void DeleteAccommodationByHost(Guid hostId)
        {
            var accommodations = _dbContext.Accommodations.Where(a => a.HostId == hostId);

            _dbContext.Accommodations.RemoveRange(accommodations);
            _dbContext.SaveChanges();
        }
    }
}
