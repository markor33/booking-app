using ReservationsLibrary.Models;

namespace ReservationsLibrary.Services
{
    public interface IAccommodationService
    {
        public IEnumerable<Accommodation> GetAll();
        public Accommodation Create(Accommodation accommodation);
        public bool IsAutoConfirmation(Guid accommodationId);
    }
}
