package accomodation.integration.subscriptionManager;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.IntegrationEvent;
import org.springframework.stereotype.Service;

import java.lang.reflect.Type;
import java.util.HashMap;
import java.util.Map;

@Service
public class SubscriptionManager implements ISubscriptionManager {

    private Map<String, Class> handlers;
    private Map<String, Class> eventTypes;

    public SubscriptionManager() {
        this.handlers = new HashMap<>();
        this.eventTypes = new HashMap<>();
    }

    @Override
    public <T extends IntegrationEvent, TH extends IIntegrationEventHandler> void addSubscription(Class<T> eventType, Class<TH> eventHandlerType) {
        handlers.put(eventType.getSimpleName(), eventHandlerType);
        eventTypes.put(eventType.getSimpleName(), eventType);
    }

    @Override
    public boolean hasSubscriptionsForEvent(String eventName) {
        return false;
    }

    @Override
    public Class getHandlerForEvent(String eventName) { return this.handlers.get(eventName); }

    @Override
    public Class getEventTypeByName(String eventName) {
        return this.eventTypes.get(eventName);
    }
}
