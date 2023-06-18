using Microsoft.Extensions.Hosting;
using Neo4j.Driver;
using RecommendationSystem.API.DTO;
using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly IAsyncSession _session;

        public AccommodationRepository(IDriver driver)
        {
            _session = driver.AsyncSession();
        }

        public async Task<List<RecommendedAccommodationDTO>> GetRecommended(Guid userId)
        {
            var cursor = await _session.RunAsync(
            "MATCH (p:Guest {id: $id})-[:HAS_RESERVATION]->(pa:Accommodation)<-[rp:GAVE_REVIEW]-(p) " +
            "WHERE pa.isDeleted = false " +
            "WITH AVG(rp.rating) AS pAvgRating " +
            "MATCH (similar:Guest)-[:HAS_RESERVATION]->(pa)<-[rs:GAVE_REVIEW]-(similar) " +
            "WITH similar, pAvgRating, AVG(rs.rating) AS sAvgRating " +
            "WHERE similar.id <> 1 AND sAvgRating >= pAvgRating AND sAvgRating <= (pAvgRating + 0.5) " +
            "MATCH (similar)-[:HAS_RESERVATION]->(a:Accommodation)<-[rp:GAVE_REVIEW]-(similar) " +
            "WHERE rp.rating >= 3 AND a.isDeleted = false " +
            "WITH a, rp " +
            "OPTIONAL MATCH (a)<-[r:GAVE_REVIEW]-(:Guest) " +
            "WHERE r.rating < 3 AND r.date >= date() - duration('P3M') " +
            "WITH a, count(r) AS badReviews, rp " +
            "WHERE badReviews < 1 " +
            "RETURN a.id as id, a.name as name, a.hostId as hostId, a.location as location, a.photo as photo, AVG(rp.rating) AS avgRating " +
            "ORDER BY avgRating DESC " +
            "LIMIT 10",
             new { id = userId.ToString() });

            var recommendedAccommodations = await cursor.
                ToListAsync(record => new RecommendedAccommodationDTO(
                    Guid.Parse(record["id"].As<string>()),
                    record["name"].As<string>(),
                    record["location"].As<string>(),
                    record["photo"].As<string>(),
                    record["avgRating"].As<float>()));

            return recommendedAccommodations;
        }

        public async Task Create(Accommodation accommodation)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "CREATE (a:Accommodation {id: $id, name: $name, hostId: $hostId, location: $location, photo: $photo, isDeleted: false})",
                        new { 
                            id = accommodation.Id.ToString(), 
                            hostId = accommodation.HostId.ToString(),
                            name = accommodation.Name,
                            location = accommodation.Location.ToString(),
                            photo = accommodation.Photo
                        });

                    return result;
                });
        }

        public async Task Delete(Guid id)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (a:Accommodation {id: $id}) SET a.isDeleted = true",
                        new { id = id.ToString() });

                    return result;
                });
        }

        public async Task<bool> SetDeleteByHost(Guid hostId, bool isDeleted)
        {
            try
            {
                var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (a:Accommodation {hostId: $hostId}) SET a.isDeleted = $isDeleted",
                        new { hostId = hostId.ToString(), isDeleted });

                    return result;
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
