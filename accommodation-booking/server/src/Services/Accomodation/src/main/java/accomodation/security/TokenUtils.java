package accomodation.security;

import javax.servlet.http.HttpServletRequest;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.ExpiredJwtException;
import io.jsonwebtoken.Jwts;

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
        return this.getClaimFromToken(token, "id");
    }

    public String getRoleFromToken(String token) {
        return this.getClaimFromToken(token, "role");
    }

    public String getClaimFromToken(String token, String name) {
        String claimValue;

        try {
            final Claims claims = this.getAllClaimsFromToken(token);
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

    public String getAuthHeaderFromHeader(HttpServletRequest request) {
        return request.getHeader(AUTH_HEADER);
    }

}