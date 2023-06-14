package accomodation.integration.events;

import accomodation.integration.IIntegrationEventHandler;
import accomodation.integration.eventBus.IEventBus;
import accomodation.model.Accomodation;
import accomodation.repository.AccomodationRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class HostSearchAccommodationsDeleteUnsuccessfulIntegrationEventHandler
        implements IIntegrationEventHandler<HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent> {

    @Autowired
    AccomodationRepository accomodationRepository;

    @Autowired
    IEventBus eventBus;

    @Override
    public void handle(HostSearchAccommodationsDeleteUnsuccessfulIntegrationEvent event) {
        List<Accomodation> accomodations = this.accomodationRepository.findByHostId(event.getHostId());
        for (Accomodation accomodation : accomodations)
            accomodation.setDeleted(false);
        this.accomodationRepository.saveAll(accomodations);

        this.eventBus.publish(new HostAccommodationsDeleteUnsuccessfulIntegrationEvent(event.getHostId()));
    }

}
