namespace Search.API.DTO
{
    public class SearchArgs
    {
        public string Location { get; set; }
        public int NumOfGuests { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public FilterArgs FilterArgs { get; set; }
    }

    public class FilterArgs
    {
        public int? MinPrice { get; set; } = null;
        public int? MaxPrice { get; set; } = null;
        public List<Guid> Benefits { get; set; } = new List<Guid>();
    }
}
