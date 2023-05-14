namespace FlightBooking.API.Infrastructure
{
    public interface IImageUploader
    {
        public string UploadImage(string img, string imgName);
        public Stream DecodeB64(string b64);
    }
}
