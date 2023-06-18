namespace RecommendationSystem.API.DTO
{
    public class RecommendedAccommodationDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public string Photo { get; private set; }
        public float AverageRating { get; private set; }

        public RecommendedAccommodationDTO(Guid id, string name, string location, string photo, float averageRating)
        {
            Id = id;
            Name = name;
            Location = location;
            Photo = photo;
            AverageRating = averageRating;
        }

    }
}
