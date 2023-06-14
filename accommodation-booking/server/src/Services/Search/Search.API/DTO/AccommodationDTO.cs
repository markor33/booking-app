using Search.API.Models;

namespace Search.API.DTO
{
    public class AccommodationDTO
    {
        public Guid Id { get; private set; }
        public Guid HostId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Address Location { get; private set; }
        public int MinGuests { get; private set; }
        public int MaxGuests { get; private set; }
        public string Photo { get; private set; }
        public List<Benefit> Benefits { get; private set; }
        public PriceType PriceType { get; private set; }
        public float Price { get; private set; }

        public AccommodationDTO(Accommodation accommodation, int numOfGuests, DateTime start, DateTime end)
        {
            Id = accommodation.Id;
            HostId = accommodation.HostId;
            Name = accommodation.Name;
            Description = accommodation.Description;
            Location = accommodation.Location;
            MinGuests = accommodation.MinGuests;
            MaxGuests = accommodation.MaxGuests;
            Photo = accommodation.Photo;
            Benefits = accommodation.Benefits;    
            PriceType = accommodation.PriceType;
            Price = accommodation.CalculatePrice(numOfGuests, start, end);
        }
    }
}
