namespace EventBus.NET.Integration.SubscriptionManager
{
    public interface ISubscriptionManager
    {
        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        bool HasSubscriptionsForEvent(string eventName);
        Type GetHandlerForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
    }
}
