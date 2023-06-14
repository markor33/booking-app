using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace ReservationsLibrary.IntegrationEvents
{
    public class HostReservationsDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; set; }

        [JsonConstructor]
        public HostReservationsDeletedIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }

    }
}
