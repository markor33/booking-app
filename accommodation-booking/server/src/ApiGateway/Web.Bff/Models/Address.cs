using System.Text.Json.Serialization;

namespace Web.Bff.Models
{
    public class Address
    {
        public string Country { get;  set; } = string.Empty;
        public string City { get;  set; } = string.Empty;
        public string Street { get;  set; } = string.Empty;
        public string Number { get;  set; } = string.Empty;

        public Address(string country, string city, string street, string number)
        {
            Country = country;
            City = city;
            Street = street;
            Number = number;
        }

    }
}
