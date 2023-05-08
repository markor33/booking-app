using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure
{
    public class PriceRepository : EntityRepository<Price>, IPriceRepository
    {
        public PriceRepository(ReservationsDbContext dbContext) : base(dbContext) { }
    }
}
