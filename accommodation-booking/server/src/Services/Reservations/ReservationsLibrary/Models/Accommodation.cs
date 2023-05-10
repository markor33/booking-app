namespace ReservationsLibrary.Models
{
    public class Accommodation : BaseEntityModel
    {
        public Guid HostId { get; set; }
        public bool AutoConfirmation { get; set; }

        public Accommodation() { }

        public Accommodation(Guid id, Guid hostId, bool autoConfiramtion)
        {
            Id = id;
            HostId = hostId;
            AutoConfirmation = autoConfiramtion;
        }
    }
}
