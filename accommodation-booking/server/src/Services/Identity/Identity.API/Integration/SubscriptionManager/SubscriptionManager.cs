namespace Identity.API.Integration.SubscriptionManager
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly Dictionary<string, Type> _handlers;
        private readonly List<Type> _eventTypes;

        public SubscriptionManager()
        {
            _handlers = new Dictionary<string, Type>();
            _eventTypes = new List<Type>();
        }

        public void AddSubcription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            if (HasSubscriptionsForEvent(eventName))
                return;

            _handlers.Add(eventName, typeof(TH));
            _eventTypes.Add(typeof(T));
        }

        public Type GetHandlerForEvent(string eventName) => _handlers[eventName];

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

    }
}
