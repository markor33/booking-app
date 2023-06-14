using Reservations.API.Infrasructure.Base;
using ReservationsLibrary.Models;

namespace Reservations.API.Infrasructure
{
    public interface IAccommodationRepository : IEntityRepository<Accommodation>
    {
        public Task<bool> DeleteAccommodationByHost(Guid hostId);
        public Task<List<Accommodation>> GetByHost(Guid hostId);
    }
}
