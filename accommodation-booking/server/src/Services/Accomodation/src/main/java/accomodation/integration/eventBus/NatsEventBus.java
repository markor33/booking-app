package accomodation.integration.eventBus;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.IntegrationEvent;
import accomodation.integration.events.*;
import accomodation.integration.subscriptionManager.ISubscriptionManager;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.util.StdDateFormat;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import io.nats.client.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.nio.charset.StandardCharsets;

@Service
public class NatsEventBus implements IEventBus {
    @Autowired
    private ApplicationContext applicationContext;
    @Autowired
    private Connection connection;
    @Autowired
    private ISubscriptionManager subscriptionManager;
    private ObjectMapper objectMapper =  new ObjectMapper();;

    @PostConstruct
    public void initializeNats() {
        this.objectMapper.registerModule(new JavaTimeModule());
        this.objectMapper.setDateFormat(new StdDateFormat().withColonInTimeZone(true));
        this.objectMapper.enable(SerializationFeature.WRITE_ENUMS_USING_INDEX);

        this.subscribe(HostReservationsDeletedIntegrationEvent.class, HostReservationsDeletedIntegrationEventHandler.class);
        this.subscribe(HostAccommodationsDeleteFromRecommendationSystemUnsuccessfulIntegrationEvent.class,
                HostAccommodationsDeleteFromRecommendationSystemUnsuccessfulIntegrationEventHandler.class);
    }

    public void publish(IntegrationEvent event) {
        String eventName = event.getClass().getSimpleName();

        String json = null;
        try {
            json = this.objectMapper.writeValueAsString(event);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
        byte[] payload = json.getBytes(StandardCharsets.UTF_8);
        this.connection.publish(eventName, payload);
    }

    public <T extends IntegrationEvent, TH extends IIntegrationEventHandler> void subscribe(Class<T> eventType, Class<TH> eventHandler) {
        String eventName = eventType.getSimpleName();
        this.subscriptionManager.addSubscription(eventType, eventHandler);
        Dispatcher d = this.connection.createDispatcher((msg) -> {
            String receivedMessage = new String(msg.getData(), StandardCharsets.UTF_8);
            this.processEvent(msg.getSubject(), receivedMessage);
        });

        d.subscribe(eventName);
    }

    private void processEvent(String eventName, String message) {
        Class eventType = this.subscriptionManager.getEventTypeByName(eventName);
        Class eventHandlerType = this.subscriptionManager.getHandlerForEvent(eventName);
        try {
            IntegrationEvent event = (IntegrationEvent)this.objectMapper.readValue(message, eventType);
            IIntegrationEventHandler handler = (IIntegrationEventHandler) this.applicationContext.getBean(eventHandlerType);
            handler.handle(event);
        }
        catch (Exception e) {
            e.printStackTrace();
        }
    }

}
