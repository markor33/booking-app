using Search.API.Models;

namespace Search.API.DTO
{
    public class SearchArgs
    {
        public string Location { get; set; }
        public int NumOfGuests { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
