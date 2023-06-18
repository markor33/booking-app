using Neo4j.Driver;
using RecommendationSystem.API.Models;

namespace RecommendationSystem.API.Persistence
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IAsyncSession _session;

        public ReservationRepository(IDriver driver)
        {
            _session = driver.AsyncSession();
        }

        public async Task Create(Reservation reservation)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (p:Guest {id: $personId}) " +
                        "MATCH (a:Accommodation {id: $accommodationId}) " +
                        "CREATE (p)-[:HAS_RESERVATION {id: $reservationId}]->(a)",
                        new { 
                            personId = reservation.GuestId.ToString(), 
                            accommodationId = reservation.AccommodationId.ToString(), 
                            reservationId = reservation.Id.ToString() 
                        });

                    return result;
                });
        }

        public async Task Delete(Reservation reservation)
        {
            var result = await _session.ExecuteWriteAsync(
                tx =>
                {
                    var result = tx.RunAsync(
                        "MATCH (p:Guest {id: $personId})-" +
                        "[r:HAS_RESERVATION {id: $reservationId}]->" +
                        "(a:Accommodation {id: $accommodationId}) " +
                        "DELETE r",
                        new
                        {
                            personId = reservation.GuestId.ToString(),
                            accommodationId = reservation.AccommodationId.ToString(),
                            reservationId = reservation.Id.ToString()
                        });

                    return result;
                });
        }
    }
}
