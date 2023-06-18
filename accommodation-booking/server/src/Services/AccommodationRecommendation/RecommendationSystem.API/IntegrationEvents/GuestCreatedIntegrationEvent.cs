using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class GuestCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid GuestId { get; private set; }
        public string Name { get; private set; }

        [JsonConstructor]
        public GuestCreatedIntegrationEvent(Guid guestId, string name)
        {
            GuestId = guestId;
            Name = name;
        }
    }
}
