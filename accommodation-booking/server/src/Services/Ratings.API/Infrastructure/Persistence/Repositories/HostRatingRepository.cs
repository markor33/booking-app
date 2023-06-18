using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class HostRatingRepository : EntityRepository<HostRating>, IHostRatingRepository
    {
        public HostRatingRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public List<HostRating> GetAllByHost(Guid hostId)
        {
            return _dbContext.HostRatings.Where(rating => rating.HostId == hostId).ToList();
        }

        public List<int> GetAllGradesByGuest(Guid guestId)
        {
            return _dbContext.HostRatings.Where(rating => rating.GuestId == guestId).Select(x => x.Grade).ToList();
        }

        public double GetAverageGradeByHost(Guid hostId)
        {
            if(!_dbContext.HostRatings.Where(r => r.HostId == hostId).Any())
                return 0;

            var avg = _dbContext.HostRatings.Where(rating => rating.HostId == hostId)
                                            .Average(rating => rating.Grade);
            return avg;
        }

        public HostRating GetByGuestAndHost(Guid guestId, Guid hostId)
        {
            return _dbContext.HostRatings.FirstOrDefault(rating => rating.GuestId == guestId && rating.HostId == hostId);
        }

        public HostRating GetByReservationId(Guid reservationId)
        {
            return _dbContext.HostRatings.FirstOrDefault(rating => rating.ReservationId == reservationId);
        }

        public List<int> GetGradesByGuest(Guid guestId)
        {
            var reservations = _dbContext.Reservations.Where(r => r.GuestId == guestId).OrderBy(r => r.Period.Start).ToList();
            var list = new List<int>();
            foreach (Reservation r in reservations)
            {
                var rating = _dbContext.HostRatings.FirstOrDefault(rat => rat.ReservationId == r.Id);
                if (rating != null)
                    list.Add(rating.Grade);
                else
                    list.Add(0);
            }
            return list;
        }

        public List<int> GetGradesByHost(Guid hostId)
        {
            var reservations = _dbContext.Reservations.Where(r => r.HostId == hostId).OrderBy(r => r.Period.Start).ToList();
            var list = new List<int>();
            foreach (Reservation r in reservations)
            {
                var rating = _dbContext.HostRatings.FirstOrDefault(rat => rat.ReservationId == r.Id);
                if (rating != null)
                    list.Add(rating.Grade);
                else
                    list.Add(0);
            }
            return list;
        }
    }
}
