package accomodation.integration.eventBus;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.IntegrationEvent;

public interface IEventBus {
    <T extends IntegrationEvent, TH  extends IIntegrationEventHandler> void subscribe();
    void publish(IntegrationEvent event);
}
