package accomodation.security;

import java.text.ParseException;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.ExpiredJwtException;
import io.jsonwebtoken.Jwts;
import com.nimbusds.jwt.JWT;
import com.nimbusds.jwt.JWTParser;

@Component
public class TokenUtils {

    @Value("${app.secret-key}")
    public String SECRET;

    @Value("Authorization")
    private String AUTH_HEADER;

    public String getToken(HttpServletRequest request) {
        String authHeader = getAuthHeaderFromHeader(request);

        if (authHeader != null && authHeader.startsWith("Bearer ")) {
            return authHeader.substring(7);
        }

        return null;
    }

    public String getIdFromToken(String token) {
        return this.getClaimFromToken(token, "sub");
    }

    public String getRoleFromToken(String token) {
        return this.getClaimFromToken(token, "role");
    }

    public String getClaimFromToken(String token, String name) {
        String claimValue;

        try {
            Map<String, Object> claims = this.getClaims(token);
            claimValue = String.valueOf(claims.get(name));
        } catch (ExpiredJwtException ex) {
            throw ex;
        } catch (Exception e) {
            claimValue = null;
        }
        return claimValue;
    }

    private Claims getAllClaimsFromToken(String token) {
        Claims claims;
        try {
            claims = Jwts.parser()
                    .setSigningKey(SECRET)
                    .parseClaimsJws(token)
                    .getBody();
        } catch (ExpiredJwtException ex) {
            throw ex;
        } catch (Exception e) {
            e.printStackTrace();
            claims = null;
        }

        return claims;
    }
    
    private Map<String, Object> getClaims(String token) {
    	JWT jwt;
		try {
			jwt = JWTParser.parse(token);
	    	return jwt.getJWTClaimsSet().getClaims();
		} catch (ParseException e) {
			throw new IllegalArgumentException("Invalid JWT token", e);
		}

    }

    public String getAuthHeaderFromHeader(HttpServletRequest request) {
        return request.getHeader(AUTH_HEADER);
    }

}