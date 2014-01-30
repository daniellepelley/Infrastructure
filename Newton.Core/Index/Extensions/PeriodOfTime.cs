using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Dates;

namespace Newton.Extensions
{
    public static class PeriodOfTimeExtensions
    {
        /// <summary>
        /// Returns whether or not the passed date is within the period of Time
        /// </summary>
        public static bool IsWithin(this IPeriodOfTime source, DateTime dateTime)
        {
            return
                source.Start.HasValue &&
                source.End.HasValue &&
                dateTime >= source.Start.Value &&
                dateTime < source.End.Value;
        }

        /// <summary>
        /// Returns whether or not the passed date is within the period of Time
        /// </summary>
        public static bool IsWithin(this IPeriodOfTime source, DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return source.IsWithin(dateTime.Value);
            return false;
        }

        /// <summary>
        /// Sets the end date on each period of time to the start date of the next period of time
        /// </summary>
        public static void SetEndDateFromNextStartDate(this IEnumerable<IPeriodOfTime> source, DateTime end)
        {
            var periods = source.OrderBy(p => p.Start).ToList();

            for (int i = 0; i < periods.Count - 1; i++)
            {
                periods[i].End = periods[i + 1].Start;
            }

            periods.LastOrDefault().End = end;
        }

        public static IEnumerable<Tuple<IPeriodOfTime, IPeriodOfTime>> Conflicts(this IEnumerable<IPeriodOfTime> source, IEnumerable<IPeriodOfTime> otherPeriodOfTime)
        {
            PeriodOfTimeConflictComparer comparer = new PeriodOfTimeConflictComparer();

            return (from s1 in source
                    from s2 in otherPeriodOfTime
                    where comparer.Equals(s1, s2)
                    select new Tuple<IPeriodOfTime, IPeriodOfTime>(s1, s2));
        }
    }

    public class PeriodOfTimeConflictComparer : EqualityComparer<IPeriodOfTime>
    {
        public override bool Equals(IPeriodOfTime x, IPeriodOfTime y)
        {
            if (!x.Start.HasValue ||
                !x.End.HasValue ||
                !y.Start.HasValue ||
                !y.End.HasValue)
            {
                return false;
            }


            return
                (x.Start >= y.Start && x.Start < y.End) ||
                (x.End < y.End && x.End > y.Start) ||
                (y.Start >= x.Start && y.Start < x.End) ||
                (y.End < x.End && y.End > x.Start);
        }

        private bool Check(DateTime? dateTime, IPeriodOfTime periodOfTime)
        {
            return
                dateTime.HasValue &&
                periodOfTime.Start.HasValue &&
                periodOfTime.End.HasValue &&
                dateTime.Value >= periodOfTime.Start.Value &&
                dateTime.Value <= periodOfTime.End.Value;
        }

        public override int GetHashCode(IPeriodOfTime obj)
        {
            return obj.GetHashCode();
        }
    }
}
