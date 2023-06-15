using Ratings.Models;

namespace RatingsLibrary.Models
{
    public class AccommodationRating : BaseEntityModel
    {
        private int _grade;
        public Guid GuestId { get; set; }
        public Guid AccommodationId { get; set; }
        public int Grade
        {
            get { return _grade; }
            set
            {
                if (value >= 1 && value <= 5)
                    _grade = value;
                else
                    throw new ArgumentOutOfRangeException("Grade must be between 1 and 5.");
            }
        }
        public DateTime DateTimeOfGrade { get; set; } = DateTime.Now;
    }
}
