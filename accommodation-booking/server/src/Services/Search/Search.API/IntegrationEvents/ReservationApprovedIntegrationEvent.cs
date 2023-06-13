﻿using EventBus.NET.Integration;
using Search.API.Models;
using System.Text.Json.Serialization;

namespace Search.API.IntegrationEvents
{
    public class ReservationApprovedIntegrationEvent : IntegrationEvent
    {
        public Guid ReservationId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid GuestId { get; private set; }
        public DateRange Period { get; private set; }

        [JsonConstructor]
        public ReservationApprovedIntegrationEvent(Guid reservationId, Guid accommodationId, Guid guestId, DateRange period)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            GuestId = guestId;
            Period = period;
        }

    }
}
