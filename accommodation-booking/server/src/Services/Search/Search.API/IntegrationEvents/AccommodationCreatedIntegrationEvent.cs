using EventBus.NET.Integration;
using Search.API.Models;
using System.Text.Json.Serialization;

namespace Search.API.IntegrationEvents
{
    public class AccommodationCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid AccommodationId { get; private set; }
        public Guid HostId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Address Location { get; private set; }
        public int MinGuests { get; private set; }
        public int MaxGuests { get; private set; }
        public string Photo { get; private set; }
        public List<Benefit> Benefits { get; private set; }
        public PriceType PriceType { get; private set; }
        public float GeneralPrice { get; private set; }
        public int WeekendIncrease { get; private set; }

        [JsonConstructor]
        public AccommodationCreatedIntegrationEvent(Guid accommodationId, Guid hostId, string name, string description, Address location,
            int minGuests, int maxGuests, string photo, List<Benefit> benefits,
            PriceType priceType, float generalPrice, int weekendIncrease)
        {
            AccommodationId = accommodationId;
            HostId = hostId;
            Name = name;
            Description = description;
            Location = location;
            MinGuests = minGuests;
            MaxGuests = maxGuests;
            Photo = photo;
            Benefits = benefits;
            PriceType = priceType;
            GeneralPrice = generalPrice;
            WeekendIncrease = weekendIncrease;
        }
    }
}
