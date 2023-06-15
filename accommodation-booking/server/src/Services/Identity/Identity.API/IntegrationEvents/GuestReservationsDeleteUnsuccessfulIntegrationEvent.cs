using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Identity.API.IntegrationEvents
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
