using Microsoft.EntityFrameworkCore;
using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : EntityRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckCancelationRateLessThenFive(Guid hostId)
        {
            var numOfCanceledForHost = (double)  _dbContext.Reservations.Count(r => r.Canceled && r.HostId == hostId);
            var allReservationsForHost = (double)  _dbContext.Reservations.Count(r => r.HostId == hostId) * 100;
            return (numOfCanceledForHost/ allReservationsForHost * 100) < 5;
        }

        public Reservation GetReservationByGuestAndAccommodationInPast(Guid guestId, Guid accommodationId)
        {
            return _dbContext.Reservations.FirstOrDefault(res => res.Period.End < DateTime.Now && res.GuestId == guestId &&
                    res.AccommodationId == accommodationId && res.Canceled == false);
        }

        public Reservation GetReservationByGuestAndHostInPast(Guid guestId, Guid hostId)
        {
            return _dbContext.Reservations.FirstOrDefault(res => res.Period.End < DateTime.Now && res.GuestId == guestId &&
                    res.HostId == hostId && res.Canceled == false);
        }
        public async Task<Reservation> CancelAsync(Guid id)
        {
            var res = await _dbContext.Reservations.FirstOrDefaultAsync(_r => _r.Id == id);
            res.Canceled = true;
            _dbContext.Reservations.Update(res);
            await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<Reservation> CreateAsync(Reservation res)
        {
            await _dbContext.Reservations.AddAsync(res);
            await _dbContext.SaveChangesAsync();
            return res;
        }
    }
}
