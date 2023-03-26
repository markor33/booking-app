using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace FlightBooking.API.Identity.Models
{
    [CollectionName("AppRoles")]
    public class ApplicationRole: MongoIdentityRole<Guid>
    {

        public ApplicationRole() { }

        public ApplicationRole(string roleName): base(roleName) { }

    }
}
