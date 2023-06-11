namespace Identity.API.Integration.Events
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public Task HandleAsync(TestIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
