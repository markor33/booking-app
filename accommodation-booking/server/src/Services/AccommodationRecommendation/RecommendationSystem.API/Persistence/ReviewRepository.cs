using Neo4j.Driver;
using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IAsyncSession _session;

        public ReviewRepository(IDriver driver)
        {
            _session = driver.AsyncSession();
        }

        public async Task Create(Review review)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (p:Guest {id: $personId}) " +
                        "MATCH (a:Accommodation {id: $accommodationId}) " +
                        "CREATE (p)-[:GAVE_REVIEW {id: $reviewId, rating: $rating, date: date($date)}]->(a)",
                        new
                        {
                            personId = review.GuestId.ToString(),
                            accommodationId = review.AccommodationId.ToString(),
                            reviewId = review.Id.ToString(),
                            rating = review.Rating,
                            date = review.Date.ToString("yyyy-MM-dd")
                        });

                    return result;
                });
        }

        public async Task Delete(Review review)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (p:Guest {id: $personId})-" +
                        "[r:GAVE_REVIEW {id: $reviewId}]->" +
                        "(a:Accommodation {id: $accommodationId}) " +
                        "DELETE r",
                        new
                        {
                            personId = review.GuestId.ToString(),
                            accommodationId = review.AccommodationId.ToString(),
                            reviewId = review.Id.ToString(),
                        });

                    return result;
                });
        }

        public async Task Update(Review review)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (p:Guest {id: $personId})-" +
                        "[r:GAVE_REVIEW {id: $reviewId}]->" +
                        "(a:Accommodation {id: $accommodationId}) " +
                        "SET r.rating = $rating, r.date = date($date)",
                        new
                        {
                            personId = review.GuestId.ToString(),
                            accommodationId = review.AccommodationId.ToString(),
                            reviewId = review.Id.ToString(),
                            rating = review.Rating,
                            date = review.Date.ToString("yyyy-MM-dd")
                        });

                    return result;
                });
        }
    }
}
