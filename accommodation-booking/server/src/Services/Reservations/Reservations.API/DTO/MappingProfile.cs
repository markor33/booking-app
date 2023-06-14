using AutoMapper;
using ReservationsLibrary.Models;

namespace Reservations.API.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReservationRequestDTO, ReservationRequest>();

            CreateMap<Reservation, ReservationDTO>();
        }
    }
}
