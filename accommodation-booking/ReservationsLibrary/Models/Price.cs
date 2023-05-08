using ReservationsLibrary.Enums;

namespace ReservationsLibrary.Models
{
    public class Price : BaseEntityModel
    {
        public float Amount { get; set; }
        public PriceType Type { get; set; }

        public Price() { }

        public Price(Guid id, float amount, PriceType type)
        {
            Id = id;
            Amount = amount;
            Type = type;
        }
    }
}
