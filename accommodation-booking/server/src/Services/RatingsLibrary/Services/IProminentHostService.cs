namespace RatingsLibrary.Services
{
    public interface IProminentHostService
    {
        void UpdateGradeAcceptableForHost(Guid hostId);
        void UpdateCancellationRateAcceptableForHost(Guid hostId);
        void UpdateHasFiveReservationsForHost(Guid hostId);
        void UpdateDurationOfReservationsAcceptableForHost(Guid hostId);
        bool IsHostProminent(Guid hostId);
    }
}
