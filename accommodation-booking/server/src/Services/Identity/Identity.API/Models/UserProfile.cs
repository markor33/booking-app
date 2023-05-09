namespace Identity.API.Models
{
    public class UserProfile
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
    }   
}
