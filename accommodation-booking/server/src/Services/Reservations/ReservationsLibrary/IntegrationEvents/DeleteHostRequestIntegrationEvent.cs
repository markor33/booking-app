using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class DeleteHostRequestIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public DeleteHostRequestIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
