using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class Benefit
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        [JsonConstructor]
        public Benefit(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
