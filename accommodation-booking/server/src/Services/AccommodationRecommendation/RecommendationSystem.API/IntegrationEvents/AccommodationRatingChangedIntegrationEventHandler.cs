using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class AccommodationRatingChangedIntegrationEventHandler : IIntegrationEventHandler<AccommodationRatingChangedIntegrationEvent>
    {
        private readonly IReviewRepository _reviewRepository;

        public AccommodationRatingChangedIntegrationEventHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task HandleAsync(AccommodationRatingChangedIntegrationEvent @event)
        {
            await _reviewRepository.Update(new Review(@event.ReviewId, @event.GuestId, @event.AccommodationId, @event.Rating, DateOnly.FromDateTime(@event.CreationDate)));
        }
    }
}
