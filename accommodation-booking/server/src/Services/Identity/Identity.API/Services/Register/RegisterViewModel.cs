using Identity.API.Models;

namespace Identity.API.Services.Register
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
    }

    public enum UserType
    {
        HOST,
        GUEST
    }
}
