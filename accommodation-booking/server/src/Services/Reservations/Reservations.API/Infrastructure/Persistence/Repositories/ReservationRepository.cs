using Microsoft.EntityFrameworkCore;
using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

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

        public void DeleteAllReservationsByGuest(Guid guestId)
        {
            var itemsToDelete = _dbContext.Reservations.Where(e => e.GuestId == guestId);

            _dbContext.Reservations.RemoveRange(itemsToDelete);
            _dbContext.SaveChanges();
        }

        public void DeleteReservationsByHost(Guid hostId)
        {
            var reservations = _dbContext.Reservations.Include(r => r.Accommodation).Where(r => r.Accommodation.HostId == hostId);

            _dbContext.Reservations.RemoveRange(reservations);
            _dbContext.SaveChanges();
        }

        public bool IsOverLappedByAccomodation(DateRange range, Guid accommodationId)
        {
            var numOfReservation = _dbContext.Reservations.Where(e => e.Period.Start < range.End && e.Period.End > range.Start
                                          && e.AccommodationId == accommodationId && e.Canceled == false).GroupBy(r => r.Id).Count();
            return numOfReservation != 0;
        }
    }
}
