using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Identity.API.IntegrationEvents
{
    public class GuestReservationsDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid GuestId { get; private set; }

        [JsonConstructor]
        public GuestReservationsDeletedIntegrationEvent(Guid guestId)
        {
            GuestId = guestId;
        }
    }
}
