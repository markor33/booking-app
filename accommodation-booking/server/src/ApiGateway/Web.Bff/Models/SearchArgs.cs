namespace Web.Bff.Models
{
    public class SearchArgs
    {
        public string Location { get; set; }
        public int NumOfGuests { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public PriceRange PriceRange { get; set; }
        public List<Benefit> Benefits { get; set; }
        public bool IsHostProminent { get; set; }
    }
}
