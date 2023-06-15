using System.Text.Json.Serialization;

namespace Search.API.Models
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange() { }

        [JsonConstructor]
        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static int GetNumberOfWeekends(DateTime startDate, DateTime endDate)
        {
            int daysDifference = (endDate - startDate).Days;
            int fullWeeks = daysDifference / 7;
            int remainingDays = daysDifference % 7;

            int weekendsCount = fullWeeks * 2;

            DateTime lastFullWeekStart = startDate.AddDays(fullWeeks * 7);

            for (int i = 0; i <= remainingDays; i++)
            {
                DateTime currentDate = lastFullWeekStart.AddDays(i);
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekendsCount += 1;
                }
            }

            return weekendsCount;
        }

    }
}
