package accomodation.integration;

public interface IIntegrationEventHandler<TIntegrationEvent> {
    void handle(TIntegrationEvent event);
}
