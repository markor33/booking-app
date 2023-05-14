using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace FlightBooking.API.Identity.Models
{
    [CollectionName("AppUsers")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {

    }
}
