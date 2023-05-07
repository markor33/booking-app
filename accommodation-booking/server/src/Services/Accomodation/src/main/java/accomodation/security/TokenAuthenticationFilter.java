package accomodation.security;

import java.io.IOException;
import java.util.UUID;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.filter.OncePerRequestFilter;

import accomodation.security.model.Role;
import accomodation.security.model.User;
import io.jsonwebtoken.ExpiredJwtException;

public class TokenAuthenticationFilter extends OncePerRequestFilter {

    private TokenUtils tokenUtils;

    public TokenAuthenticationFilter(TokenUtils tokenHelper) {
        this.tokenUtils = tokenHelper;
    }

    @Override
    public void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain chain)
            throws IOException, ServletException {
        String authToken = tokenUtils.getToken(request);
        try {
            if (authToken != null) {
            		String id = tokenUtils.getIdFromToken(authToken);
            		String role = tokenUtils.getRoleFromToken(authToken);
            		System.out.println("User id: " + id + ", Role: " + role);
            		User u = new User(UUID.fromString(id), new Role(UUID.randomUUID(), role));
                    //OVDE POSLATI ID I ROLE PREKO gRPC-A
            		
                    /*
                        if ( SVE JE OKEJ ) {
	                        TokenBasedAuthentication authentication = new TokenBasedAuthentication(u);
	                        authentication.setToken(authToken);
	                        SecurityContextHolder.getContext().setAuthentication(authentication);
	                   }
                    */               
            }

        } catch (ExpiredJwtException ex) {
        	
        }
        chain.doFilter(request, response);
    }

}