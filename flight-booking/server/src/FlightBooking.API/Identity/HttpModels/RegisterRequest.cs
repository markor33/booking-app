using FlightBooking.Business.Entities;

namespace FlightBooking.API.Identity.HttpModels
{
    public class RegisterRequest
    {
        public User User { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
