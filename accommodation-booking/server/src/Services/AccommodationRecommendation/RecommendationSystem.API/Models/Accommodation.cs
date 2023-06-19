namespace RecommendationSystem.API.Models
{
    public class Accommodation
    {
        public Guid Id { get; private set; }
        public Guid HostId { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public string Photo { get; private set; }
        public bool IsDeleted { get; set; }

        public Accommodation(Guid id, Guid hostId, string name, string location, string photo)
        {
            Id = id;
            HostId = hostId;
            Name = name;
            Location = location.ToString();
            Photo = photo;
            IsDeleted = false;
        }

    }
}
