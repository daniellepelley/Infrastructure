using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Extensions
{
    public abstract class PublicHolidays : IPublicHolidays
    {
        /// <summary>
        /// Returns all public holidays for the passed year.
        /// </summary>
        public DateTime[] GetPublicHolidays(int fromYear, int toYear)
        {
            List<DateTime> dates = new List<DateTime>();
            for (int i = fromYear; i <= toYear; i++)
            {
                dates.AddRange(GetPublicHolidays(i));
            }
            return dates.ToArray();
        }

        public abstract DateTime[] GetPublicHolidays(int year);
    }
}