using Microsoft.EntityFrameworkCore;
using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure.Persistence.Repositories
{
    public class ReservationRepository : EntityRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationsDbContext dbContext) : base(dbContext) { }

        public int NumOfCanceledReservationForGuest(Guid guestId) => _dbContext.Reservations.Where(r => r.GuestId == guestId && r.Canceled == true).GroupBy(r => r.GuestId).Count();

        public Reservation GetById(Guid resId) => _dbContext.Reservations.Include(a => a.Accommodation).Include(p => p.Price).FirstOrDefault(r => r.Id == resId);

        public bool ActiveGuestReservations(Guid guestId)
        {
            var number = _dbContext.Reservations.Where(r => r.GuestId == guestId && r.Canceled == false && r.Period.Start > DateTime.Now).GroupBy(r => r.GuestId).Count();
            return number != 0;
        }

        public bool ActiveHostReservations(Guid hostId)
        {
            var number = _dbContext.Reservations.Include(r => r.Accommodation)
                        .Where(r => r.Accommodation.HostId == hostId && r.Canceled == false && r.Period.Start > DateTime.Now).GroupBy(r => r.Accommodation.HostId).Count();
            return number != 0;
        }
    }
}
