package accomodation.integration.subscriptionManager;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.IntegrationEvent;

import java.lang.reflect.Type;

public interface ISubscriptionManager {
    <T extends IntegrationEvent, TH extends IIntegrationEventHandler>
        void addSubscription(Class<T> eventType, Class<TH> eventHandlerType);
    boolean hasSubscriptionsForEvent(String eventName);
    Class getHandlerForEvent(String eventName);
    Class getEventTypeByName(String eventName);
}
