using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure
{
    public class AccommodationRepository : EntityRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(ReservationsDbContext dbContext) : base(dbContext) { }
    }
}
