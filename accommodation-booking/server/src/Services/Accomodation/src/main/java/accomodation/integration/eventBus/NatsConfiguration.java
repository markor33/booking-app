package accomodation.integration.eventBus;

import io.nats.client.Connection;
import io.nats.client.Nats;
import io.nats.client.Options;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.io.IOException;
import java.time.Duration;

@Configuration
public class NatsConfiguration {

    @Value("${nats.serverUrl}")
    private String natsServerUrl;

    @Bean(destroyMethod = "close")
    public Connection natsConnection() throws IOException, InterruptedException {
        Options options = new Options.Builder()
                .server(natsServerUrl)
                .connectionTimeout(Duration.ofSeconds(5))
                .build();
        return Nats.connect(options);
    }
}
