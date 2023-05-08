package accomodation.security;

import java.io.IOException;
import java.util.UUID;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.filter.OncePerRequestFilter;

import accomodation.security.model.Role;
import accomodation.security.model.User;

public class TokenAuthenticationFilter extends OncePerRequestFilter {

    private TokenUtils tokenUtils;

    public TokenAuthenticationFilter(TokenUtils tokenHelper) {
        this.tokenUtils = tokenHelper;
    }
    
    public boolean validate(String authHeader) {
		RestTemplate restTemplate = new RestTemplate();

		String url = "http://host.docker.internal:10000/api/identity/auth/validate";

		HttpHeaders headers = new HttpHeaders();
		headers.add("Authorization", authHeader);

		HttpEntity<String> requestEntity = new HttpEntity<>(headers);
		try {
			ResponseEntity<String> responseEntity = restTemplate.exchange(url, HttpMethod.GET, requestEntity, String.class);
			return true;
		} catch (Exception e) {
			e.printStackTrace();
			return false;
		}
    }
    
    @Override
    public void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        String authToken = tokenUtils.getToken(request);
        String value = request.getHeader("Authorization");
        try {
            if (authToken != null) {
            		String id = tokenUtils.getIdFromToken(authToken);
            		String role = tokenUtils.getRoleFromToken(authToken);
            		System.out.println("User id: " + id + ", Role: " + role);
            		User u = new User(UUID.fromString(id), new Role(UUID.randomUUID(), role));
            		if(validate(value)) {
            			TokenBasedAuthentication authentication = new TokenBasedAuthentication(u);
                        authentication.setToken(authToken);
                        SecurityContextHolder.getContext().setAuthentication(authentication);
            		}
            		
            }

        } catch (Exception ex) {
        	
        }
        chain.doFilter(request, response);
    }

}