using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure.Persistence.Repositories
{
    public class AccommodationRepository : EntityRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(ReservationsDbContext dbContext) : base(dbContext) { }
    }
}
