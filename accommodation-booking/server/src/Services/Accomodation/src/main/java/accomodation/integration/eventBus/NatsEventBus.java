package accomodation.integration.eventBus;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.IntegrationEvent;
import accomodation.integration.events.TestIntegrationEvent;
import accomodation.integration.events.TestIntegrationEventHandler;
import accomodation.integration.subscriptionManager.ISubscriptionManager;
import accomodation.integration.subscriptionManager.SubscriptionManager;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.PropertyNamingStrategy;
import com.fasterxml.jackson.databind.util.StdDateFormat;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import io.nats.client.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.io.IOException;
import java.nio.charset.StandardCharsets;

@Service
public class NatsEventBus {
    @Autowired
    private ApplicationContext applicationContext;
    @Autowired
    private Connection connection;
    @Autowired
    private ISubscriptionManager subscriptionManager;
    private ObjectMapper objectMapper =  new ObjectMapper();;

    @PostConstruct
    public void initializeNats() throws IOException, InterruptedException {
        this.objectMapper.registerModule(new JavaTimeModule());
        this.objectMapper.setDateFormat(new StdDateFormat().withColonInTimeZone(true));

        this.subscribe(TestIntegrationEvent.class, TestIntegrationEventHandler.class);
    }

    public void publish(IntegrationEvent event) throws JsonProcessingException {
        String eventName = event.getClass().getSimpleName();
        System.out.println(eventName);

        String json = this.objectMapper.writeValueAsString(event);
        System.out.println(json);
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
            System.out.println(eventHandlerType.getSimpleName());
            IIntegrationEventHandler handler = (IIntegrationEventHandler) this.applicationContext.getBean(eventHandlerType);
            handler.handle(event);
        }
        catch (Exception e) {
            e.printStackTrace();
        }
    }

}
