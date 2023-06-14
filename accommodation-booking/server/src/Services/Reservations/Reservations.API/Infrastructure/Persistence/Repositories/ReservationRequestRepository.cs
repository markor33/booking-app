using Microsoft.EntityFrameworkCore;
using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Data;
using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace Reservations.API.Infrasructure.Persistence.Repositories
{
    public class ReservationRequestRepository : EntityRepository<ReservationRequest>, IReservationRequestRepository
    {
        public ReservationRequestRepository(ReservationsDbContext dbContext) : base(dbContext) { }

        public List<ReservationRequest> GetByHost(Guid hostId)
        {
            return _dbContext.ReservationRequests.Include(r => r.Accommodation).Where(r => r.Accommodation.HostId == hostId && r.Status == ReservationRequestStatus.ON_HOLD).ToList();
        }
        public List<ReservationRequest> GetByGuest(Guid guestId)
        {
            return _dbContext.ReservationRequests.Include(r => r.Accommodation).Where(r => r.GuestId == guestId && r.Status == ReservationRequestStatus.ON_HOLD).ToList();
        }

        public List<ReservationRequest> GetOverLapped(DateRange range, Guid accommodationId)
        {
            return _dbContext.ReservationRequests.Where(e => e.IsDeleted == false && e.Period.Start < range.End && e.Period.End > range.Start
                                            && e.AccommodationId == accommodationId && e.Status == ReservationRequestStatus.ON_HOLD).ToList();
        }
        public ReservationRequest GetById(Guid requestId) => _dbContext.ReservationRequests.Include(a => a.Accommodation).FirstOrDefault(r => r.Id == requestId);

        public void DeleteAllRequestsByGuest(Guid guestId)
        {
            var itemsToDelete = _dbContext.ReservationRequests.Where(e => e.GuestId == guestId);

            _dbContext.ReservationRequests.RemoveRange(itemsToDelete);
            _dbContext.SaveChanges();
        }

        public void DeleteReservationRequestsByHost(Guid hostId)
        {
            var requests = _dbContext.ReservationRequests.Include(r => r.Accommodation).Where(r => r.Accommodation.HostId == hostId);

            _dbContext.ReservationRequests.RemoveRange(requests);
            _dbContext.SaveChanges();
        }
    }
}
