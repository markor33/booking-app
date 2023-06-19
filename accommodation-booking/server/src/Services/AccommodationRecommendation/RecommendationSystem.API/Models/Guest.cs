namespace RecommendationSystem.API.Models
{
    public class Guest
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Guest(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
