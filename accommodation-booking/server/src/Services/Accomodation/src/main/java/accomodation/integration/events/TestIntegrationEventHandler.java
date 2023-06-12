package accomodation.integration.events;

import accomodation.integration.IIntegrationEventHandler;
import org.springframework.stereotype.Service;

@Service
public class TestIntegrationEventHandler implements IIntegrationEventHandler<TestIntegrationEvent> {

    @Override
    public void handle(TestIntegrationEvent testIntegrationEvent) {
        System.out.println(testIntegrationEvent.getName());
    }

}
