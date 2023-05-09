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
            return _dbContext.ReservationRequests
<<<<<<< HEAD:accommodation-booking/server/src/Services/Reservations/Reservations.API/Infrastructure/Persistence/Repositories/ReservationRequestRepository.cs
                    .Include(r => r.Accommodation)
                    .Include(p => p.Price)
                    .Where(r => r.Accommodation.HostId == hostId && r.Status == ReservationRequestStatus.ON_HOLD).ToList();

=======
                    .Include(r => r.Accommodation).Include(p => p.Price)
                    .Where(r => r.Accommodation.HostId == hostId && r.Status == ReservationRequestStatus.ON_HOLD).ToList();
 
>>>>>>> feature/edit_user_profile:accommodation-booking/server/src/Services/Reservations/Reservations.API/Infrasructure/ReservationRequestRepository.cs
        }

        public List<ReservationRequest> GetOverLapped(DateRange range, Guid accommodationId)
        {
            return _dbContext.ReservationRequests.Where(e => e.Period.Start < range.End && e.Period.End > range.Start
                                            && e.AccommodationId == accommodationId && e.Status == ReservationRequestStatus.ON_HOLD).ToList();
        }
        public ReservationRequest GetById(Guid requestId) => _dbContext.ReservationRequests.Include(a => a.Accommodation).Include(f => f.Price).FirstOrDefault(r => r.Id == requestId);
    }
}
