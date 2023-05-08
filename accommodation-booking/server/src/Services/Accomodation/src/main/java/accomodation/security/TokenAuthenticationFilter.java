package accomodation.security;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.net.http.HttpResponse.BodyHandlers;
import java.util.UUID;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.filter.OncePerRequestFilter;

import accomodation.security.model.Role;
import accomodation.security.model.User;

public class TokenAuthenticationFilter extends OncePerRequestFilter {

    private TokenUtils tokenUtils;

    public TokenAuthenticationFilter(TokenUtils tokenHelper) {
        this.tokenUtils = tokenHelper;
    }
    
    public boolean validate(String value) {
    	System.out.println("->");
    	System.out.println(value);
    	HttpClient client = HttpClient.newHttpClient();
		HttpRequest req = HttpRequest.newBuilder()
				.uri(URI.create("http://localhost:10000/api/identity/auth/validate"))
				.header("Authorization", value)
				.build();
		HttpResponse<String> resp;
		try {
			resp = client.send(req, BodyHandlers.ofString());
			System.out.println(resp);
			System.out.println(resp.statusCode());
			if(resp.statusCode() == 200) {
				return true;
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
		return false;
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
            		System.out.println("testtest");
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