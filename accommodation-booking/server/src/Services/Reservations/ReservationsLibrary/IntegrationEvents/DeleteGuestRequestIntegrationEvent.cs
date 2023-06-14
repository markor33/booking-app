using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class DeleteGuestRequestIntegrationEvent : IntegrationEvent
    {
        public Guid GuestId { get; private set; }

        [JsonConstructor]
        public DeleteGuestRequestIntegrationEvent(Guid guestId)
        {
            GuestId = guestId;
        }
    }
}
