using Identity.API.Integration.SubscriptionManager;
using NATS.Client;
using System.Text;
using System;
using System.Text.Json;

namespace Identity.API.Integration.EventBus
{
    public class NatsEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IServiceProvider _serviceProvider;

        public NatsEventBus(
            IConnection connection,
            ISubscriptionManager subscriptionManager,
            IServiceProvider serviceProvider)
        {
            _connection = connection;
            _subscriptionManager = subscriptionManager;
            _serviceProvider = serviceProvider;
        }

        public void Publish(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name;
            var messageBytes = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            _connection.Publish(eventName, messageBytes);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _subscriptionManager.AddSubscription<T, TH>();
            var eventName = typeof(T).Name;
            IAsyncSubscription subscription = _connection.SubscribeAsync(eventName, ConsumerReceived);
        }

        private async void ConsumerReceived(object sender, MsgHandlerEventArgs args)
        {
            var eventName = args.Message.Subject;
            var message = Encoding.UTF8.GetString(args.Message.Data);

            await ProcessEvent(eventName, message);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            var handlerType = _subscriptionManager.GetHandlerForEvent(eventName);
            var eventType = _subscriptionManager.GetEventTypeByName(eventName);

            await using var scope = _serviceProvider.CreateAsyncScope();
            var handler = scope.ServiceProvider.GetService(handlerType);
            if (handler == null) return;
            var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

            await Task.Yield();
            await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
        }


    }
}
