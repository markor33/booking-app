using System;

namespace Web.Bff.Models
{
    public class AccomodationDTO
    {
        public string Id { get; set; }
        public string HostId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinGuests { get; set; }
        public int MaxGuests { get; set; }
        public int WeekendIncrease { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public Address Location { get; set; }
        public List<Benefit> Benefits { get; set; }
        public List<Photo> Photos { get; set; }
        public float GeneralPrice { get; set; }
        public PriceType PriceType { get; set; }
        public bool AutoConfirmation { get; set; }
        public List<AccommodationRatingDTO> AccommRatings { get; set; }
        public List <HostRatingDTO> HostRatings { get; set; }
        public bool IsHostProminent { get; set; }
        public double AvgHostGrade { get; set; }
        public double AvgAccommGrade { get; set; }
    }
}
