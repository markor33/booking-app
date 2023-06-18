using Ratings.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatingsLibrary.Models
{
    public class ProminentHost : BaseEntityModel
    {
        private bool _isGradeAcceptable = false;
        private bool _isCancellationRateAcceptable = false;
        private bool _hasFiveReservations = false;
        private bool _isDurationOfReservationsAcceptable = false;

        public Guid HostId { get; set; }
        public bool IsGradeAcceptable
        {
            get { return _isGradeAcceptable; }
            set
            {
                _isGradeAcceptable = value;
                UpdateIsHostProminent();
            }
        }

        public bool IsCancellationRateAcceptable
        {
            get { return _isCancellationRateAcceptable; }
            set
            {
                _isCancellationRateAcceptable = value;
                UpdateIsHostProminent();
            }
        }
        public bool HasFiveReservations
        {
            get { return _hasFiveReservations; }
            set
            {
                _hasFiveReservations = value;
                UpdateIsHostProminent();
            }
        }
        public bool IsDurationOfReservationsAcceptable
        {
            get { return _isDurationOfReservationsAcceptable; }
            set
            {
                _isDurationOfReservationsAcceptable = value;
                UpdateIsHostProminent();
            }
        }
        [NotMapped]
        public bool IsHostProminent { get; set; } = false;

        private void UpdateIsHostProminent()
        {
            IsHostProminent = IsGradeAcceptable && IsCancellationRateAcceptable && HasFiveReservations && IsDurationOfReservationsAcceptable;
        }
    }
}
