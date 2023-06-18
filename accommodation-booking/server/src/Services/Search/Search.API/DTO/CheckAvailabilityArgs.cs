namespace Search.API.DTO
{
    public class CheckAvailabilityArgs
    {
        public Guid Id { get; set; }
        public int NumOfGuests { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
