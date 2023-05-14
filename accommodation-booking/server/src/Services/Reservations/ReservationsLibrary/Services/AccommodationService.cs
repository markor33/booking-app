using Reservations.API.Infrasructure;
using ReservationsLibrary.Models;

namespace ReservationsLibrary.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public IEnumerable<Accommodation> GetAll() => _accommodationRepository.GetAll();

        public Accommodation Create(Accommodation accommodation) => _accommodationRepository.Create(accommodation);

        public bool IsAutoConfirmation(Guid accommodationId)
        {
            var accommodation = _accommodationRepository.GetById(accommodationId);
            return accommodation.AutoConfirmation; 
        }

        public void DeleteAccommodationByHost(Guid hostId) => _accommodationRepository.DeleteAccommodationByHost(hostId);
    }
}
