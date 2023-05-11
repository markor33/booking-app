package accomodation.util;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

import org.springframework.stereotype.Component;

import com.cloudinary.Cloudinary;
import com.cloudinary.utils.ObjectUtils;

@Component
public class PhotoUploader {
	
	private Cloudinary cloudinary;
	
	public PhotoUploader() {
		Map config = new HashMap();
		config.put("cloud_name", "dso3mvk4p");
		config.put("api_key", "277748894623968");
		config.put("api_secret", "Qa8PFjoGBl1xXfdQVO7MmeUgt-M");
		cloudinary = new Cloudinary(config);
	}
	
	 public String uploadImage(String b64) {
	        Cloudinary cloudinary = new Cloudinary(ObjectUtils.asMap(
	            "cloud_name", "dso3mvk4p",
	            "api_key", "277748894623968",
	            "api_secret", "Qa8PFjoGBl1xXfdQVO7MmeUgt-M"));
	        
	        try {
				Map result = cloudinary.uploader().upload(b64, ObjectUtils.emptyMap());		
				return (String) result.get("secure_url");
			} catch (IOException e) {
				e.printStackTrace();
				return "Error";
			}
	    }
	 	
}
