using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;


namespace Newton.Extensions
{
    public static class Dates
    {
        /// <summary>
        /// If this datetime in null, will set this to the passed datetime.
        /// </summary>
        public static DateTime? Overlay(this DateTime? thisDateTime, DateTime? dateTime)
        {
            if (!thisDateTime.HasValue)
                return dateTime;
            else
                return thisDateTime.Value.Overlay(dateTime);
        }

        /// <summary>
        /// If this datetime in null, will set this to the passed datetime.
        /// </summary>
        public static DateTime Overlay(this DateTime thisDateTime, DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return thisDateTime;
            else
                return thisDateTime.Overlay(dateTime.Value);
        }

        /// <summary>
        /// If this datetime in null, will set this to the passed datetime.
        /// </summary>
        public static DateTime Overlay(this DateTime thisDateTime, DateTime dateTime)
        {
            if (thisDateTime == DateTime.MinValue)
                return dateTime;
            else
                return thisDateTime;
        }




        /// <summary>
        /// Adds business days to the date.
        /// </summary>
        public static DateTime? AddBusinessDays(this DateTime? date, int businessDays)
        {
            if (date == null) { return null; }
            return date.Value.AddBusinessDays(businessDays);
        }

        /// <summary>
        /// Adds business days to the date.
        /// </summary>
        public static DateTime AddBusinessDays(this DateTime date1, int businessDays)
        {
            return AddBusinessDays(date1, businessDays, new UKPublicHolidays());
        }

        /// <summary>
        /// Adds business days to the date.
        /// </summary>
        public static DateTime AddBusinessDays(this DateTime date1, int businessDays, IPublicHolidays publicHolidays)
        {
            //Whether to add or subtract days.
            int adjustment = (businessDays > 0 ? 1 : -1);

            if (businessDays > 30)
            {
                DateTime[] publicHolidayDates = publicHolidays.GetPublicHolidays(date1.Year, date1.Year + 1 + (businessDays / 260));

                for (int n = 1; n <= Math.Abs(businessDays); n++)
                {
                    do
                    {
                        date1 = date1.AddDays(adjustment);
                    }
                    while (!IsBusinessDay(date1, publicHolidayDates));
                }
            }
            else
            {
                for (int n = 1; n <= Math.Abs(businessDays); n++)
                {
                    do
                    {
                        date1 = date1.AddDays(adjustment);
                    }
                    while (!IsBusinessDay(date1));
                }
            }

            return date1;
        }

        #region DayOfWeek

        /// <summary>
        /// Returns the next week day
        /// </summary>
        public static DateTime AddWeekDays(this DateTime date, int numberOfDays)
        {
            int wholeWeeksInDays = (numberOfDays / 5) * 7;
            int partialWeeksInDays = numberOfDays % 5;

            if ((int)date.DayOfWeek <= 5 - partialWeeksInDays)
            {
                return date.AddDays(wholeWeeksInDays + partialWeeksInDays);
            }
            else
            {
                return date.AddDays(wholeWeeksInDays + partialWeeksInDays + 2);
            }
        }

        /// <summary>
        /// Checks if the date passed is a week day.
        /// </summary>
        public static bool IsWeekDay(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            return true;
        }

        /// <summary>
        /// Returns the next week day
        /// </summary>
        public static DateTime NextWeekDay(this DateTime date)
        {
            int dayOfWeek = (int)date.DayOfWeek;

            if (dayOfWeek >= 5)
                return date.AddDays(8 - dayOfWeek);
 
            return date.AddDays(1);
        }

        /// <summary>
        /// Amends the date to the next date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime NextDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            return NextDayOfWeek(date, dayOfWeek, false);
        }

        /// <summary>
        /// Amends the date to the next date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime NextDayOfWeek(this DateTime date, DayOfWeek dayOfWeek, bool excludeThisDate)
        {
            DateTime date1 = date;
            if (excludeThisDate)
                date1 = date1.AddDays(1);

            while (date1.DayOfWeek != dayOfWeek)
                date1 = date1.AddDays(1);
            return date1;
        }

        /// <summary>
        /// Amends the date to the previous date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime PreviousDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            return PreviousDayOfWeek(date, dayOfWeek, false);
        }

        /// <summary>
        /// Amends the date to the previous date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime PreviousDayOfWeek(this DateTime date, DayOfWeek dayOfWeek, bool excludeThisDate)
        {
            DateTime date1 = date;
            if (excludeThisDate)
                date1 = date1.AddDays(-1);

            while (date1.DayOfWeek != dayOfWeek)
                date1 = date1.AddDays(-1);
            return date1;
        }

        /// <summary>
        /// Amends the date to the next date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime? NextDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek)
        {
            return NextDayOfWeek(date, dayOfWeek, false);
        }

        /// <summary>
        /// Amends the date to the next date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime? NextDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek, bool excludeThisDate)
        {
            if (date != null)
                return NextDayOfWeek(date.Value, dayOfWeek, excludeThisDate);
            return null;
        }

        /// <summary>
        /// Amends the date to the preceeding date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime? PreviousDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek)
        {
            return PreviousDayOfWeek(date, dayOfWeek, false);
        }

        /// <summary>
        /// Amends the date to the preceeding date with the same DayOfWeek as passed.
        /// </summary>
        public static DateTime? PreviousDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek, bool excludeThisDate)
        {
            if (date != null)
                return PreviousDayOfWeek(date.Value, dayOfWeek, excludeThisDate);
            return null;
        }

        #endregion

        /// <summary>
        /// Checks if the date falls within the date range.
        /// </summary>
        public static bool IsWithinRange(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return (date >= startDate &&
                date <= endDate);
        }

        /// <summary>
        /// Returns dates that fall within the date range.
        /// </summary>
        public static IEnumerable<DateTime> WithinRange(this IEnumerable<DateTime> dates, DateTime startDate, DateTime endDate)
        {
            return dates.Where(d => d.IsWithinRange(startDate, endDate));
        }

        #region AreSame

        /// <summary>
        /// Whether or not the date is the same month.
        /// </summary>
        public static bool AreSameMonth(this DateTime source, DateTime date)
        {
            return (source.Year == date.Year &&
                source.Month == date.Month);
        }

        /// <summary>
        /// Whether or not the date is the same day.
        /// </summary>
        public static bool AreSameDay(this DateTime source, DateTime date)
        {
            return (source.Year == date.Year &&
                source.Month == date.Month &&
                source.Day == date.Day);
        }

        /// <summary>
        /// Whether or not the date is the same hour.
        /// </summary>
        public static bool AreSameHour(this DateTime source, DateTime date)
        {
            return (source.Year == date.Year &&
                source.Month == date.Month &&
                source.Day == date.Day &&
                source.Hour == date.Hour);
        }

        /// <summary>
        /// Whether or not the date is the same minute.
        /// </summary>
        public static bool AreSameMinute(this DateTime source, DateTime date)
        {
            return (source.Year == date.Year &&
                source.Month == date.Month &&
                source.Day == date.Day &&
                source.Hour == date.Hour &&
                source.Minute == date.Minute);
        }

        /// <summary>
        /// Whether or not the date is the same second.
        /// </summary>
        public static bool AreSameSecond(this DateTime source, DateTime date)
        {
            return (source.Year == date.Year &&
                source.Month == date.Month &&
                source.Day == date.Day &&
                source.Hour == date.Hour &&
                source.Minute == date.Minute &&
                source.Second == date.Second);
        }

        #endregion

        # region BusinessDays

        /// <summary>
        /// If the date is not business day, returns the next date in the future
        /// which is a business day.
        /// </summary>
        public static DateTime AdvanceToBusinessDay(this DateTime date)
        {
            while (!IsBusinessDay(date))
            {
                date = date.AddDays(1);
            }
            return date;
        }

        /// <summary>
        /// Checks if the date passed is a business day.
        /// </summary>
        public static bool IsBusinessDay(this DateTime source)
        {
            return source.IsWeekDay() && 
                !BusinessDays.IsPublicHoliday(source);
        }

        /// <summary>
        /// Checks if the date passed is a business day.
        /// </summary>
        public static bool IsBusinessDay(this DateTime source, IEnumerable<DateTime> publicHolidays)
        {
            return source.IsWeekDay() &&
                publicHolidays.Where(d => source.AreSameDay(d)).Count() > 0;
        }

        /// <summary>
        /// Counts the business days between two dates.
        /// </summary>
        public static int CountBusinessDays(this DateTime date1, DateTime date2)
        {
            return BusinessDays.NumberOfDays(date1, date2);
        }

        ///// <summary>
        ///// Adds number of business days to the date.
        ///// </summary>
        //public static DateTime TodayPlusBusinessDays(int businessDays, string time)
        //{
        //    DateTime dateTime = DateTime.Now.AddBusinessDays(businessDays);
        //    return dateTime.SetTime(time);
        //}

        /// <summary>
        /// Adds business days to the date.
        /// </summary>
        public static DateTime? RemoveTime(this DateTime? date)
        {
            if (date == null) { return null; }
            return date.Value.RemoveTime();
        }

        public static DateTime RemoveTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        ///// <summary>
        ///// Adds business days to the date.
        ///// </summary>
        //public static DateTime? SetTime(this DateTime? date, string time)
        //{
        //    if (date == null) { return null; }
        //    return date.Value.SetTime(time);
        //}

        ///// <summary>
        ///// Sets the time on the date.
        ///// </summary>
        //public static DateTime SetTime(this DateTime date, string time)
        //{
        //    return (date.ToString("dd/MM/yyyy " + time).ToDate());
        //}

        # endregion

        #region Convert

        ///// <summary>
        ///// Converts the date into a string.
        ///// </summary>
        //public static string ToString(this DateTime? dateTime, string format)
        //{
        //    if (!dateTime.HasValue)
        //        return string.Empty;
        //    else
        //        return dateTime.Value.ToString(format);
        //}

        /// <summary>
        /// Converts the date into a string.
        /// </summary>
        public static string ToWeek(this DateTime dateTime, string format)
        {
            return ToWeek(dateTime, format, 0);
        }

        /// <summary>
        /// Converts the date into a string.
        /// </summary>
        public static string ToWeek(this DateTime dateTime, string format, int offset)
        {
            return ToWeek(dateTime, System.Globalization.CultureInfo.CurrentCulture.Calendar, format, offset);
        }

        /// <summary>
        /// Converts the date into a string.
        /// </summary>
        public static string ToWeek(this DateTime dateTime, System.Globalization.Calendar calendar, string format, int offset)
        {
            string week = (calendar.GetWeekOfYear(dateTime, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday) + offset).ToString();
            string str = dateTime.ToString(format).Replace("w", "W");
            return str.Replace("WW", week.PadLeft(2, (char)48)).Replace("W", week);
        }

        #endregion


        /// <summary>
        /// Returns the maximum date.
        /// </summary>
        public static DateTime MaxDate(this DateTime thisDate, DateTime date)
        {
            if (thisDate > date)
                return thisDate;
            return date;
        }

        #region Set

        /// <summary>
        /// Sets the year value on a date.
        /// </summary>
        public static DateTime SetYear(this DateTime thisDate, int year)
        {
            return new DateTime(year, thisDate.Month, thisDate.Day, thisDate.Hour, thisDate.Minute, thisDate.Second, thisDate.Millisecond);
        }

        /// <summary>
        /// Sets the month value on a date.
        /// </summary>
        public static DateTime SetMonth(this DateTime thisDate, int month)
        {
            return new DateTime(thisDate.Year, month, thisDate.Day, thisDate.Hour, thisDate.Minute, thisDate.Second, thisDate.Millisecond);
        }

        /// <summary>
        /// Sets the day value on a date.
        /// </summary>
        public static DateTime SetDay(this DateTime thisDate, int day)
        {
            return new DateTime(thisDate.Year, thisDate.Month, day, thisDate.Hour, thisDate.Minute, thisDate.Second, thisDate.Millisecond);
        }

        /// <summary>
        /// Sets the hour value on a date.
        /// </summary>
        public static DateTime SetHour(this DateTime thisDate, int hour)
        {
            return new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, hour, thisDate.Minute, thisDate.Second, thisDate.Millisecond);
        }

        /// <summary>
        /// Sets the minute value on a date.
        /// </summary>
        public static DateTime SetMinute(this DateTime thisDate, int minute)
        {
            return new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, thisDate.Hour, minute, thisDate.Second, thisDate.Millisecond);
        }

        /// <summary>
        /// Sets the second value on a date.
        /// </summary>
        public static DateTime SetSecond(this DateTime thisDate, int second)
        {
            return new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, thisDate.Hour, thisDate.Minute, second, thisDate.Millisecond);
        }

        /// <summary>
        /// Sets the millisecond value on a date.
        /// </summary>
        public static DateTime SetMillisecond(this DateTime thisDate, int millisecond)
        {
            return new DateTime(thisDate.Year, thisDate.Month, thisDate.Day, thisDate.Hour, thisDate.Minute, thisDate.Second, millisecond);
        }



        #endregion
    }
}
