using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Extensions
{
    /// <summary>
    /// A class used to manage business days.
    /// </summary>
    public static class BusinessDays
    {
        /// <summary>
        /// Calculates the number of working days in between two dates.
        /// </summary>
        public static int NumberOfDays(DateTime date1, DateTime date2)
        {
            int maximumExtraDays = 6 - (int)date1.DayOfWeek;

            TimeSpan timeSpan = (date2 - date1);

            int weeks = (timeSpan.Days / 7);
            int extraDays = (timeSpan.Days % 7) + (timeSpan.Hours > 0 ? 1 : 0);

            if (extraDays == maximumExtraDays + 1
                || extraDays == maximumExtraDays + 2)
                extraDays = maximumExtraDays;
            else if (extraDays > maximumExtraDays + 2)
                extraDays = extraDays - 2;

            int businessDays = (weeks * 5) + extraDays;

            int bankHolidays = NumberBankHolidays(date1, date2);

            return businessDays - bankHolidays;
        }

        /// <summary>
        /// Checks if the date falls within the date range.
        /// </summary>
        /// <remarks>
        /// Not yet working.
        /// </remarks>
        public static int NumberBankHolidays(DateTime startDate, DateTime endDate)
        {
            return NumberBankHolidays(startDate, endDate, new UKPublicHolidays());
        }

        /// <summary>
        /// Checks if the date falls within the date range.
        /// </summary>
        /// <remarks>
        /// Not yet working.
        /// </remarks>
        public static int NumberBankHolidays(DateTime startDate, DateTime endDate, IPublicHolidays publicHolidays)
        {
            int numberOfDays = 0;

            if (startDate.Year == endDate.Year)
            {
                return publicHolidays.GetPublicHolidays(startDate.Year).WithinRange(startDate, endDate).Count();
            }

            int[] years = GetYears(startDate, endDate);

            foreach (int year in years)
            {
                if (year == startDate.Year ||
                    year == endDate.Year)
                {
                    numberOfDays += publicHolidays.GetPublicHolidays(year).WithinRange(startDate, endDate).Count();
                }
                else
                    numberOfDays += publicHolidays.GetPublicHolidays(year).Length;
            }
            return numberOfDays;
        }

        /// <summary>
        /// Checks if the date falls within the date range.
        /// </summary>
        public static int[] GetYears(DateTime startDate, DateTime endDate)
        {
            List<int> ints = new List<int>();
            for (int i = startDate.Year; i <= endDate.Year; i++)
            {
                ints.Add(i);
            }
            return ints.ToArray();
        }

        #region Public Holidays

        /// <summary>
        /// Whether the passed date is a public holiday.
        /// </summary>
        public static bool IsPublicHoliday(this DateTime dateTime)
        {
            return IsPublicHoliday(dateTime, new UKPublicHolidays());
        }

        /// <summary>
        /// Whether the passed date is a public holiday.
        /// </summary>
        public static bool IsPublicHoliday(this DateTime dateTime, IPublicHolidays publicHolidays)
        {
            foreach (DateTime holiday in publicHolidays.GetPublicHolidays(dateTime.Year))
            {
                if (dateTime.AreSameDay(holiday))
                    return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Returns the number of days between two days of the weeks.
        /// </summary>
        public static int NumberOfDays(DayOfWeek day1, DayOfWeek day2)
        {
            int day1Int = (int)day1;
            int day2Int = (int)day2;

            if (day2Int >= day1Int)
                return day2Int - day1Int;
            else
                return day2Int - day1Int + 7;
        }

        /// <summary>
        /// Whether or not the passed year is a leap year.
        /// </summary>
        public static bool IsLeapYear(int year)
        {
            return year % 4 == 0;
        }
    }
}