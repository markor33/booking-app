using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Notifications.SignalR.IntegrationEvents.Notifications
{
    public class HostProminentStatusChanged : IntegrationEvent
    {
        public Guid HostId { get; private set; }
        public bool IsProminent { get; private set; }

        [JsonConstructor]
        public HostProminentStatusChanged(Guid hostId, bool isProminent)
        {
            HostId = hostId;
            IsProminent = isProminent;
        }
    }
}
