using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace FlightBooking.API.Infrastructure
{
    public class ImageUploader : IImageUploader
    {

        private static Cloudinary? cloudinary;

        public const string CLOUD_NAME = "disvuvajt";
        public const string API_KEY = "757544832169673";
        public const string API_SECRET = "yQ_9ainT52yyv6BzmH2RC8z1ACY";

        public string UploadImage(string imgUrl, string imgName)
        {
            var account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);

            var stream = DecodeB64(imgUrl);
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imgName, stream),
                    PublicId = imgName + "_adixu"
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                return uploadResult.SecureUrl.ToString();

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "ERROR";
            }  
        }

        public Stream DecodeB64(string b64)
        {
            var temp = b64.Substring(b64.LastIndexOf(',') + 1);
            byte[] newBytes = Convert.FromBase64String(temp);
            Stream stream = new MemoryStream(newBytes);
            return stream;
        }
    }
}
