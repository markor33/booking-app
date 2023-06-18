namespace Web.Bff.Models
{
    public class SearchExtended
    {
        public Guid Id { get;  set; }
        public Guid HostId { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public Address Location { get;  set; }
        public int MinGuests { get;  set; }
        public int MaxGuests { get;  set; }
        public string Photo { get;  set; }
        public List<Benefit> Benefits { get;  set; }
        public PriceType PriceType { get;  set; }
        public float Price { get;  set; }
        public bool IsHostProminent { get; set; }
        public double AvgHostGrade { get; set; }
        public double AvgAccommGrade { get; set; }
    }
}
