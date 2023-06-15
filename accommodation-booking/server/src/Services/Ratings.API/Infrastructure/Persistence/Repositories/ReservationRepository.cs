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
            var cancelationRate = (double)_dbContext.Reservations.Count(r => r.Canceled && r.HostId == hostId) / _dbContext.Reservations.Count(r => r.HostId == hostId) * 100;
            return cancelationRate < 5;
        }

        public bool CheckReservationCountInPastForHost(Guid hostId)
        {
            return _dbContext.Reservations.Count(res => res.Period.End <= DateTime.Now && res.HostId == hostId) > 5;
        }

        public bool CheckReservationDurationInPastForHost(Guid hostId)
        {
            return _dbContext.Reservations.Where(r => r.HostId == hostId && r.Period.End < DateTime.Now)
                                          .Sum(r => (int)(r.Period.End - r.Period.Start).TotalDays) > 50;
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
    }
}
