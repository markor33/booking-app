using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models
{
    public class UserProfile
    {
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Address Address { get; set; }
    }   
}
