package accomodation.integration.eventBus;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.IntegrationEvent;
import org.springframework.stereotype.Service;

@Service
public interface IEventBus {
    <T extends IntegrationEvent, TH  extends IIntegrationEventHandler> void subscribe(Class<T> eventType, Class<TH> eventHandler);
    void publish(IntegrationEvent event);
}
