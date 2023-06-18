using EventBus.NET.Integration;
using ReservationsLibrary.Utils;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class ReservationApprovedForRatingsIntegrationEvent : IntegrationEvent
    {
        public Guid ReservationId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public Guid GuestId { get; private set; }
        public Guid HostId { get; private set; }
        public DateRange Period { get; private set; }
        public bool Canceled { get; private set; }

        [JsonConstructor]
        public ReservationApprovedForRatingsIntegrationEvent(Guid reservationId, Guid accommodationId, Guid guestId, DateRange period, Guid hostId, bool canceled)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            GuestId = guestId;
            Period = period;
            HostId = hostId;
            Canceled = canceled;
        }

    }
}