using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class GuestReservationsDeleteUnsuccessfulIntegrationEvent : IntegrationEvent
    {
        public Guid GuestId { get; private set; }

        [JsonConstructor]
        public GuestReservationsDeleteUnsuccessfulIntegrationEvent(Guid guestId)
        {
            GuestId = guestId;
        }

    }
}
