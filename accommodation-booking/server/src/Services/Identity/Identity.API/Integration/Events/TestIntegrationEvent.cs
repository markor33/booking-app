using System.Text.Json.Serialization;

namespace Identity.API.Integration.Events
{
    public class TestIntegrationEvent : IntegrationEvent
    {
        public string Name { get; private set; }

        [JsonConstructor]
        public TestIntegrationEvent(string name) : base()
        {
            Name = name;
        }

    }
}
