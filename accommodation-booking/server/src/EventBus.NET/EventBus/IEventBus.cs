
namespace EventBus.NET.Integration.EventBus
{
    public interface IEventBus
    {
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        void Publish(IntegrationEvent @event);
    }
}
