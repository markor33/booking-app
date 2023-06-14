namespace EventBus.NET.Integration
{
    public class IntegrationEvent
    {
        public Guid Id { get; private init; }
        public DateTime CreationDate { get; private init; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public IntegrationEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

    }
}
