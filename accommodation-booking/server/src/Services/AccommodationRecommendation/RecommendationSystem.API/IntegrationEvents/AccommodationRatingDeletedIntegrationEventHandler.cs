using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class AccommodationRatingDeletedIntegrationEventHandler : IIntegrationEventHandler<AccommodationRatingDeletedIntegrationEvent>
    {
        private readonly IReviewRepository _reviewRepository;

        public AccommodationRatingDeletedIntegrationEventHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task HandleAsync(AccommodationRatingDeletedIntegrationEvent @event)
        {
            await _reviewRepository.Delete(new Review(@event.ReviewId, @event.GuestId, @event.AccommodationId, @event.Rating, DateOnly.FromDateTime(@event.CreationDate)));
        }
    }
}
