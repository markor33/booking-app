namespace Search.API.Models
{
    public class Accommodation
    {
        public Guid Id { get; private set; }
        public Guid HostId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Address Location { get; private set; }
        public int MinGuests { get; private set; }
        public int MaxGuests { get; private set; }
        public string Photo { get; private set; }
        public List<Benefit> Benefits { get; private set; }
        public List<PriceInterval> PriceIntervals { get; private set; }
        public List<Reservation> Reservations { get; private set; }
        public PriceType PriceType { get; private set; }
        public float GeneralPrice { get; private set; }
        public int WeekendIncrease { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public Accommodation(Guid id, Guid hostId, string name, string description, Address location, 
            int minGuests, int maxGuests, string photo, List<Benefit> benefits,
            PriceType priceType, float generalPrice, int weekendIncrease)
        {
            Id = id;
            HostId = hostId;
            Name = name;
            Description = description;
            Location = location;
            MinGuests = minGuests;
            MaxGuests = maxGuests;
            Photo = photo;
            Benefits = benefits;
            PriceType = priceType;
            GeneralPrice = generalPrice;
            WeekendIncrease = weekendIncrease;
            PriceIntervals = new List<PriceInterval>();
            Reservations = new List<Reservation>();
        }

        public void AddReservation(Reservation reservation) => Reservations.Add(reservation);

        public void RemoveReservation(Guid reservationId)
        {
            var reservation = Reservations.First(r => r.Id == reservationId);
            Reservations.Remove(reservation);
        }

        public void AddPriceInterval(PriceInterval interval) => PriceIntervals.Add(interval);

        public void UpdatePriceInterval(Guid priceIntervalId, float amount, DateRange period)
        {
            var priceInterval = PriceIntervals.First(pi => pi.Id == priceIntervalId);
            priceInterval.Update(amount, period);
        }

        public float CalculatePrice(int numGuests, DateTime startDate, DateTime endDate)
        {
            float generalAmount = 0;
            int days = (endDate - startDate).Days;

            if (PriceType == PriceType.PER_GUEST)
            {
                generalAmount += GeneralPrice * numGuests * days;
            }
            else
            {
                generalAmount += GeneralPrice * days;
            }

            float weekendAmount = DateRange.GetNumberOfWeekends(startDate, endDate) * WeekendIncrease;
            float additionalAmount = CalculateAdditionalPrice(startDate, endDate, numGuests);

            return generalAmount + additionalAmount + weekendAmount;
        }

        public float CalculateAdditionalPrice(DateTime startDate, DateTime endDate, int numGuests)
        {
            float additionalAmount = 0;
            List<PriceInterval> validPriceIntervals = PriceIntervals
                .Where(priceInterval => priceInterval.Interval.Start <= endDate && priceInterval.Interval.End >= startDate)
                .ToList();

            foreach (PriceInterval interval in validPriceIntervals)
            {
                DateTime intervalStart = DateTime.Compare(interval.Interval.Start, startDate) > 0 ? interval.Interval.Start : startDate;
                DateTime intervalEnd = DateTime.Compare(interval.Interval.End, endDate) < 0 ? interval.Interval.End : endDate;
                int intervalDays = (intervalEnd - intervalStart).Days;

                if (this.PriceType == PriceType.PER_GUEST)
                {
                    additionalAmount += interval.Amount * numGuests * intervalDays;
                }
                else
                {
                    additionalAmount += interval.Amount * intervalDays;
                }
            }

            return additionalAmount;
        }

        public void SetDeleted(bool isDeleted) => IsDeleted = isDeleted;

    }
}
