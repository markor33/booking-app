package accomodation.grpc.configurations;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import search.GreeterGrpc;

@Configuration
public class GreeterClientConfiguration {

    private String host = "host.docker.internal";
    private int port = 13001;

    @Bean
    public ManagedChannel managedChannel() {
        return ManagedChannelBuilder.forAddress(host, port)
                .usePlaintext()
                .build();
    }


}
