namespace Identity.API.Models
{
    public class Credentials
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
