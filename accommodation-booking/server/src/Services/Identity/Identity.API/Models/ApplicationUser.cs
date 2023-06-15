using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public UserStatus Status { get; set; } = UserStatus.ACTIVE;
    }

    [Owned]
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
    }

    public enum UserStatus
    {
        ACTIVE,
        DELETED,
        PENDING_DELETE
    }
}
