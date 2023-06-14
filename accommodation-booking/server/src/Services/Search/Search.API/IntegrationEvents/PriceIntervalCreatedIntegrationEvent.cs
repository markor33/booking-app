using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace Search.API.IntegrationEvents
{
    public class PriceIntervalCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid PriceIntervalId { get; private set; }
        public Guid AccommodationId { get; private set; }
        public float Amount { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        [JsonConstructor]
        public PriceIntervalCreatedIntegrationEvent(Guid priceIntervalId, Guid accommodationId, float amount, DateTime start, DateTime end)
        {
            PriceIntervalId = priceIntervalId;
            AccommodationId = accommodationId;
            Amount = amount;
            Start = start;
            End = end;
        }

    }
}
