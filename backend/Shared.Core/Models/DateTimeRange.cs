using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Models
{
    public class DateTimeRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            if (end < start)
                throw new ArgumentException("End date must be greater than or equal to start date.");

            Start = start;
            End = end;
        }

        public bool Contains(DateTime dateTime)
        {
            return dateTime >= Start && dateTime <= End;
        }

        public bool Overlaps(DateTimeRange other)
        {
            return Start <= other.End && End >= other.Start;
        }

        public TimeSpan Duration => End - Start;
    }
}
