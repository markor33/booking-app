using System.Text.Json.Serialization;

namespace Identity.API.Services.Login
{
    public class LoginViewModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginViewModel() { }

        [JsonConstructor]
        public LoginViewModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
