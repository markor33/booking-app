﻿using ReservationsLibrary.Enums;
using ReservationsLibrary.Utils;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.Models
{
    public class ReservationRequest : BaseEntityModel
    {
        [JsonIgnore]
        public Accommodation? Accommodation { get; set; }
        public Guid AccommodationId { get; set; }
        public Guid GuestId { get; set; }
        public DateRange? Period { get; set; }
        public int NumOfGuests { get; set; }
        public int Price { get; set; }
        public ReservationRequestStatus Status { get; set; } = ReservationRequestStatus.ON_HOLD;
        public bool IsDeleted { get; set; } = false;

        public ReservationRequest() { }

        public ReservationRequest(Guid id, Guid accommodationId, Guid guestId, int numOfGuests, int price)
        {
            Id = id;
            AccommodationId = accommodationId;
            GuestId = guestId;
            NumOfGuests = numOfGuests;
            Price = price;
        }

    }
}
