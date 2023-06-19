using Neo4j.Driver;
using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public class GuestRepository : IGuestRepository
    {
        private readonly IAsyncSession _session;

        public GuestRepository(IDriver driver)
        {
            _session = driver.AsyncSession();
        }

        public async Task Create(Guest guest)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "CREATE (a:Guest {id: $id, name: $name})",
                        new { id = guest.Id.ToString(), name = guest.Name });

                    return result;
                });
        }
    }
}
