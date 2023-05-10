﻿using Microsoft.EntityFrameworkCore;

namespace ReservationsLibrary.Utils
{
    [Owned]
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange() { }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
