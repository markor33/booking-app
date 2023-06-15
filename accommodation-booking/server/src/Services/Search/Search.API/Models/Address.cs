using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class Address
    {
        public string Country { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Street { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;

        [JsonConstructor]
        public Address(string country, string city, string street, string number)
        {
            Country = country;
            City = city;
            Street = street;
            Number = number;
        }

    }
}
