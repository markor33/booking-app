using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class PriceInterval
    {
        public Guid Id { get; private set; }
        public float Amount { get; private set; }
        public DateRange Interval { get; private set; }

        [JsonConstructor]
        public PriceInterval(Guid id, float amount, DateRange interval)
        {
            Id = id;
            Amount = amount;
            Interval = interval;
        }

        public void Update(float amount, DateRange interval)
        {
            Amount = amount;
            Interval = interval;
        }
    }
}
