namespace ReservationsLibrary.Models
{
    public class Accommodation : BaseEntityModel
    {
        public Guid HostId { get; set; }
        public List<ReservationRequest> ReservationRequests { get; set; }
        public List<Reservation> Reservations { get; set; }
        public bool AutoConfirmation { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Accommodation() { }

        public Accommodation(Guid id, Guid hostId, bool autoConfiramtion)
        {
            Id = id;
            HostId = hostId;
            AutoConfirmation = autoConfiramtion;
        }

        public void SetDelete(bool isDeleted)
        {
            IsDeleted = isDeleted;
            foreach (var reservationRequest in ReservationRequests)
                reservationRequest.IsDeleted = isDeleted;
            foreach (var reservation in Reservations)
                reservation.IsDeleted = isDeleted;
        }


    }
}
