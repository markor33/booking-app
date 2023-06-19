using Microsoft.Extensions.Hosting;
using Ratings.API.Infrasructure.Base;
using RatingsLibrary.Models;
using RatingsLibrary.Repository;
using System.Linq;

namespace Ratings.API.Infrastructure.Persistence.Repositories
{
    public class AccommodationRatingRepository : EntityRepository<AccommodationRating>, IAccommodationRatingRepository
    {
        public AccommodationRatingRepository(RatingsDbContext dbContext) : base(dbContext)
        {
        }

        public List<AccommodationRating> GetAllByAccommodation(Guid accommodationId)
        {
            return _dbContext.AccommodationRatings.Where(rating => rating.AccommodationId == accommodationId).ToList();
        }

        public double GetAverageGradeByAccommodation(Guid accommodationId)
        {
            if (!_dbContext.AccommodationRatings.Where(r => r.AccommodationId == accommodationId).Any())
                return 0;

            return _dbContext.AccommodationRatings.DefaultIfEmpty().Where(rating => rating.AccommodationId == accommodationId)
                                                    .Average(rating => rating.Grade);
        }

        public AccommodationRating GetByGuestAndAccommodation(Guid guestId, Guid accommodationId)
        {
            return _dbContext.AccommodationRatings.FirstOrDefault(rating => rating.GuestId == guestId && rating.AccommodationId == accommodationId);
        }

        public AccommodationRating GetByReservationId(Guid reservationId)
        {
            return _dbContext.AccommodationRatings.FirstOrDefault(rating => rating.ReservationId == reservationId);
        }

        public List<int> GetGradesByGuest(Guid guestId)
        {
            var reservations = _dbContext.Reservations.Where(r => r.GuestId == guestId && r.Canceled == false).OrderBy(r => r.Period.Start).ToList();
            var list = new List<int>();
            foreach(Reservation r in reservations)
            {
                var rating = _dbContext.AccommodationRatings.FirstOrDefault(rat => rat.ReservationId == r.Id);
                if (rating != null)
                    list.Add(rating.Grade);
                else
                    list.Add(0);
            }
            return list;
        }

        public List<int> GetGradesByHost(Guid hostId)
        {
            var reservations = _dbContext.Reservations.Where(r => r.HostId == hostId && r.Canceled == false).OrderBy(r => r.Period.Start).ToList();
            var list = new List<int>();
            foreach (Reservation r in reservations)
            {
                var rating = _dbContext.AccommodationRatings.FirstOrDefault(rat => rat.ReservationId == r.Id);
                if (rating != null)
                    list.Add(rating.Grade);
                else
                    list.Add(0);
            }
            return list;
        }
    }
}
