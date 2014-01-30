using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Extensions
{
    public class UKPublicHolidays : PublicHolidays
    {
        #region Methods

        /// <summary>
        /// Returns all public holidays for the passed year.
        /// </summary>
        public override DateTime[] GetPublicHolidays(int year)
        {
            List<DateTime> dates = new List<DateTime>();
            dates.Add(NewYearsHoliday(year));
            dates.AddRange(EasterHolidays(year));
            dates.Add(MayBankHoliday1(year));
            dates.Add(MayBankHoliday2(year));
            dates.Add(AugustBankHoliday(year));
            dates.AddRange(ChristmasHolidays(year));
            dates.AddRange(ExtraHolidays(year));
            return dates.ToArray();
        }

        /// <summary>
        /// Returns the New Years Day holiday for the passed year.
        /// </summary>
        public DateTime NewYearsHoliday(int year)
        {
            DateTime newYear = new DateTime(year, 1, 1);

            if ((int)newYear.DayOfWeek == 6)
            {
                return newYear.AddDays(2);
            }
            else if ((int)newYear.DayOfWeek == 0)
            {
                return newYear.AddDays(1);
            }
            else
            {
                return newYear;
            }
        }

        /// <summary>
        /// Returns the Easter holidays for the passed year.
        /// </summary>
        public DateTime[] EasterHolidays(int year)
        {
            if (year == 2009)
                return new DateTime[] {
                    new DateTime(2009, 4, 10),
                    new DateTime(2009, 4, 13) };
            else if (year == 2010)
                return new DateTime[] {
                    new DateTime(2010, 4, 2),
                    new DateTime(2010, 4, 5) };
            else if (year == 2011)
                return new DateTime[] {
                    new DateTime(2011, 4, 22),
                    new DateTime(2011, 4, 25) };
            else if (year == 2012)
                return new DateTime[] {
                    new DateTime(2012, 4, 6),
                    new DateTime(2012, 4, 9) };
            else if (year == 2013)
                return new DateTime[] {
                    new DateTime(2013, 3, 29),
                    new DateTime(2013, 4, 1) };
            else if (year == 2014)
                return new DateTime[] {
                    new DateTime(2014, 4, 18),
                    new DateTime(2014, 4, 21) };
            else if (year == 2015)
                return new DateTime[] {
                    new DateTime(2015, 4, 3),
                    new DateTime(2015, 4, 6) };
            else if (year == 2016)
                return new DateTime[] {
                    new DateTime(2016, 3, 25),
                    new DateTime(2016, 3, 29) };
            else if (year == 2017)
                return new DateTime[] {
                    new DateTime(2017, 4, 14),
                    new DateTime(2017, 4, 17) };
            else if (year == 2018)
                return new DateTime[] {
                    new DateTime(2018, 3, 30),
                    new DateTime(2018, 4, 2) };
            else if (year == 2019)
                return new DateTime[] {
                    new DateTime(2019, 4, 19),
                    new DateTime(2019, 4, 22) };
            else if (year == 2020)
                return new DateTime[] {
                    new DateTime(2020, 4, 10),
                    new DateTime(2020, 4, 13) };
            else
                return new DateTime[0];
        }

        /// <summary>
        /// Returns the first May bank holiday for the passed year.
        /// </summary>
        public DateTime MayBankHoliday1(int year)
        {
            DateTime startMay = new DateTime(year, 5, 1);
            return startMay.AddDays(BusinessDays.NumberOfDays(startMay.DayOfWeek, DayOfWeek.Monday));
        }

        /// <summary>
        /// Returns the second May bank holiday for the passed year.
        /// </summary>
        public DateTime MayBankHoliday2(int year)
        {
            if (year == 2012)
            {
                return new DateTime(2012, 6, 4);
            }
            DateTime endMay = new DateTime(year, 5, 31);
            return endMay.AddDays(-BusinessDays.NumberOfDays(DayOfWeek.Monday, endMay.DayOfWeek));
        }

        /// <summary>
        /// Returns the August bank holiday for the passed year.
        /// </summary>
        public DateTime AugustBankHoliday(int year)
        {
            DateTime endAugust = new DateTime(year, 8, 31);
            return endAugust.AddDays(-BusinessDays.NumberOfDays(DayOfWeek.Monday, endAugust.DayOfWeek));
        }

        /// <summary>
        /// Returns the Christmas holidays for the passed year.
        /// </summary>
        public DateTime[] ChristmasHolidays(int year)
        {
            DateTime christmas = new DateTime(year, 12, 25);

            if ((int)christmas.DayOfWeek == 6)
            {
                return new DateTime[] { christmas.AddDays(2), christmas.AddDays(3) };
            }
            else if ((int)christmas.DayOfWeek == 0)
            {
                return new DateTime[] { christmas.AddDays(1), christmas.AddDays(2) };
            }
            else
            {
                return new DateTime[] { christmas, christmas.AddDays(1) };
            }
        }

        /// <summary>
        /// Returns the Easter holidays for the passed year.
        /// </summary>
        public DateTime[] ExtraHolidays(int year)
        {
            if (year == 2011)
                return new DateTime[] { new DateTime(2011, 4, 29) };
            if (year == 2012)
                return new DateTime[] { new DateTime(2012, 6, 5) };
            else
                return new DateTime[0];
        }

        #endregion
    }

}