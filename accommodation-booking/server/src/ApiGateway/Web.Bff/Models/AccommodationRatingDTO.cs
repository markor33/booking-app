namespace Web.Bff.Models
{
    public class AccommodationRatingDTO
    {
        public string GuestFullName { get; set; }
        public int Grade { get; set; }
        public DateTime DateTimeOfGrade { get; set; } = DateTime.Now;
    }
}