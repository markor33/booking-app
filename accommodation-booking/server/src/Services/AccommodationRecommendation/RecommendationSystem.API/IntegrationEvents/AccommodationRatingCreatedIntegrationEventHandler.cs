using EventBus.NET.Integration;
using RecommendationSystem.API.Models;
using RecommendationSystem.API.Persistence;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class AccommodationRatingCreatedIntegrationEventHandler : IIntegrationEventHandler<AccommodationRatingCreatedIntegrationEvent>
    {
        private readonly IReviewRepository _reviewRepository;

        public AccommodationRatingCreatedIntegrationEventHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task HandleAsync(AccommodationRatingCreatedIntegrationEvent @event)
        {
            await _reviewRepository.Create(new Review(@event.ReviewId, @event.GuestId, @event.AccommodationId, @event.Rating, DateOnly.FromDateTime(@event.CreationDate)));
        }
    }
}
