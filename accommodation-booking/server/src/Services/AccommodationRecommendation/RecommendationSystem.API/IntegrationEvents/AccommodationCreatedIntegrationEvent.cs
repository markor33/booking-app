using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class AccommodationCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid AccommodationId { get; private set; }
        public Guid HostId { get; private set; }
        public string Name { get; private set; }
        public Address Location { get; private set; }
        public string Photo { get; private set; }

        [JsonConstructor]
        public AccommodationCreatedIntegrationEvent(
            Guid accommodationId, Guid hostId, string name, Address location, string photo)
        {
            AccommodationId = accommodationId;
            HostId = hostId;
            Name = name;
            Location = location;
            Photo = photo;
        }

    }
}
