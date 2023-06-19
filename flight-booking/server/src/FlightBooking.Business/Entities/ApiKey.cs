using FlightBooking.Business.Entities.Base;
using FlightBooking.Business.Helpers.CustomAttributes;

namespace FlightBooking.Business.Entities
{
    [BsonCollection("ApiKeys")]
    public class ApiKey : BaseEntity
    {
        public string UserId { get; private set; }
        public Guid Key { get; private set; }
        public DateTime? ExpireDate { get; private set; } = null;

        public ApiKey(string userId, bool isPermanent)
        {
            UserId = userId;
            Key = Guid.NewGuid();
            if (!isPermanent)
                ExpireDate = DateTime.UtcNow.AddMonths(2);
        }

        public void RefreshToken()
            => ExpireDate = DateTime.UtcNow.AddMonths(2);

        public void SetPermanent(bool isPermanent)
        {
            if (!isPermanent)
                RefreshToken();
            else
                ExpireDate = null;
        }

        public bool IsExpired()
        {
            if (ExpireDate == null)
                return false;
            return DateTime.UtcNow > ExpireDate;
        }

    }
}
