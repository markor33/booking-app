﻿using EventBus.NET.Integration;
using System.Text.Json.Serialization;

namespace RecommendationSystem.API.IntegrationEvents
{
    public class HostAccommodationsDeletedFromRecommendationSystemIntegrationEvent : IntegrationEvent
    {
        public Guid HostId { get; private set; }

        [JsonConstructor]
        public HostAccommodationsDeletedFromRecommendationSystemIntegrationEvent(Guid hostId)
        {
            HostId = hostId;
        }
    }
}
