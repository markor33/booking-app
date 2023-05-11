package accomodation.util;

import com.cloudinary.Cloudinary;
import com.cloudinary.api.ApiResponse;
import com.cloudinary.utils.ObjectUtils;

import java.util.Map;

import javax.persistence.Convert;

import org.springframework.stereotype.Component;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.Base64;
import java.util.HashMap;

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
	
	 public String uploadImage(String b64, String imgName) {
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
