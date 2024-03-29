﻿using ReservationsLibrary.Enums;
using ReservationsLibrary.Models;
using ReservationsLibrary.Utils;

namespace Reservations.API.DTO
{
    public class ReservationRequestDTO
    {
        public Guid AccommodationId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public int Price { get; set; }
    }
}
