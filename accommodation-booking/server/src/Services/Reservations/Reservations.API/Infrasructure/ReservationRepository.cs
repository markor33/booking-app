using Microsoft.EntityFrameworkCore;
using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;
using System.Reflection.Metadata.Ecma335;

namespace Reservations.API.Infrasructure
{
    public class ReservationRepository : EntityRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationsDbContext dbContext) : base(dbContext) { }

        public int NumOfCanceledReservationForGuest(Guid guestId) => _dbContext.Reservations.Where(r => r.GuestId == guestId && r.Canceled == true).GroupBy(r => r.GuestId).Count();
        
        public Reservation GetById(Guid resId) => _dbContext.Reservations.Include(a => a.Accommodation).Include(p => p.Price).FirstOrDefault(r => r.Id == resId);
    }
}
