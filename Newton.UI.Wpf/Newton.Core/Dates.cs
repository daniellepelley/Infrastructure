//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
////using System.Windows.Forms;

//using Newton.Core;

//namespace Newton.Extensions
//{
//    public static class Dates
//    {
//        /// <summary>
//        /// If this datetime in null, will set this to the passed datetime.
//        /// </summary>
//        public static DateTime? Overlay(this DateTime? thisDateTime, DateTime? dateTime)
//        {
//            if (!thisDateTime.HasValue)
//                return dateTime;
//            else
//                return thisDateTime.Value.Overlay(dateTime);
//        }

//        /// <summary>
//        /// If this datetime in null, will set this to the passed datetime.
//        /// </summary>
//        public static DateTime Overlay(this DateTime thisDateTime, DateTime? dateTime)
//        {
//            if (!dateTime.HasValue)
//                return thisDateTime;
//            else
//                return thisDateTime.Overlay(dateTime.Value);
//        }

//        /// <summary>
//        /// If this datetime in null, will set this to the passed datetime.
//        /// </summary>
//        public static DateTime Overlay(this DateTime thisDateTime, DateTime dateTime)
//        {
//            if (thisDateTime == DateTime.MinValue)
//                return dateTime;
//            else
//                return thisDateTime;
//        }




//        /// <summary>
//        /// Adds working days to the date.
//        /// </summary>
//        public static DateTime? AddWorkingDays(this DateTime? date, int workingDays)
//        {
//            if (date == null) { return null; }
//            return date.Value.AddWorkingDays(workingDays);
//        }

//        /// <summary>
//        /// Adds working days to the date.
//        /// </summary>
//        public static DateTime AddWorkingDays(this DateTime date1, int workingDays)
//        {
//            //Whether to add or subtract days.
//            int adjustment = (workingDays > 0 ? 1 : -1);

//            if (workingDays > 30)
//            {
//                DateTime[] publicHolidays = WorkingDays.PublicHolidays(date1.Year, date1.Year + 1 + (workingDays / 260));

//                for (int n = 1; n <= Math.Abs(workingDays); n++)
//                {
//                    do
//                    {
//                        date1 = date1.AddDays(adjustment);
//                    }
//                    while (!IsWorkingDay(date1, publicHolidays));
//                }
//            }
//            else
//            {
//                for (int n = 1; n <= Math.Abs(workingDays); n++)
//                {
//                    do
//                    {
//                        date1 = date1.AddDays(adjustment);
//                    }
//                    while (!IsWorkingDay(date1));
//                }
//            }

//            return date1;
//        }

//        #region DayOfWeek

//        /// <summary>
//        /// Amends the date to the next date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime AdvanceDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
//        {
//            return AdvanceDayOfWeek(date, dayOfWeek, false);
//        }

//        /// <summary>
//        /// Amends the date to the next date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime AdvanceDayOfWeek(this DateTime date, DayOfWeek dayOfWeek, bool excludeThisDate)
//        {
//            DateTime date1 = date;
//            if (excludeThisDate)
//                date1 = date1.AddDays(1);

//            while (date1.DayOfWeek != dayOfWeek)
//                date1 = date1.AddDays(1);
//            return date1;
//        }

//        /// <summary>
//        /// Amends the date to the preceeding date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime BackDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
//        {
//            return BackDayOfWeek(date, dayOfWeek, false);
//        }

//        /// <summary>
//        /// Amends the date to the preceeding date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime BackDayOfWeek(this DateTime date, DayOfWeek dayOfWeek, bool excludeThisDate)
//        {
//            DateTime date1 = date;
//            if (excludeThisDate)
//                date1 = date1.AddDays(-1);

//            while (date1.DayOfWeek != dayOfWeek)
//                date1 = date1.AddDays(-1);
//            return date1;
//        }

//        /// <summary>
//        /// Amends the date to the next date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime? AdvanceDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek)
//        {
//            return AdvanceDayOfWeek(date, dayOfWeek, false);
//        }

//        /// <summary>
//        /// Amends the date to the next date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime? AdvanceDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek, bool excludeThisDate)
//        {
//            if (date != null)
//                return AdvanceDayOfWeek(date.Value, dayOfWeek, excludeThisDate);
//            return null;
//        }

//        /// <summary>
//        /// Amends the date to the preceeding date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime? BackDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek)
//        {
//            return BackDayOfWeek(date, dayOfWeek, false);
//        }

//        /// <summary>
//        /// Amends the date to the preceeding date with the same DayOfWeek as passed.
//        /// </summary>
//        public static DateTime? BackDayOfWeek(this DateTime? date, DayOfWeek dayOfWeek, bool excludeThisDate)
//        {
//            if (date != null)
//                return BackDayOfWeek(date.Value, dayOfWeek, excludeThisDate);
//            return null;
//        }

//        #endregion

//        /// <summary>
//        /// Checks if the date falls within the date range.
//        /// </summary>
//        public static bool IsWithinRange(this DateTime date, DateTime startDate, DateTime endDate)
//        {
//            return (date >= startDate &&
//                date <= endDate);
//        }

//        /// <summary>
//        /// Returns dates that fall within the date range.
//        /// </summary>
//        public static IEnumerable<DateTime> WithinRange(this IEnumerable<DateTime> dates, DateTime startDate, DateTime endDate)
//        {
//            return dates.Where(d => d.IsWithinRange(startDate, endDate));
//        }

//        /// <summary>
//        /// Whether or not the date is the same month.
//        /// </summary>
//        public static bool IsSameMonth(this DateTime source, DateTime date)
//        {
//            return (source.Year == date.Year &&
//                source.Month == date.Month);
//        }

//        /// <summary>
//        /// Whether or not the date is the same day.
//        /// </summary>
//        public static bool IsSameDay(this DateTime source, DateTime date)
//        {
//            return (source.Year == date.Year &&
//                source.Month == date.Month &&
//                source.Day == date.Day);
//        }

//        /// <summary>
//        /// Whether the passed date is a public holiday.
//        /// </summary>
//        public static bool IsSameDayAs(this DateTime source, IEnumerable<DateTime> days)
//        {
//            foreach (DateTime day in days)
//            {
//                if (source.IsSameDay(day))
//                    return true;
//            }
//            return false;
//        }

//        /// <summary>
//        /// Whether or not the date is the same hour.
//        /// </summary>
//        public static bool IsSameHour(this DateTime source, DateTime date)
//        {
//            return (source.Year == date.Year &&
//                source.Month == date.Month &&
//                source.Day == date.Day &&
//                source.Hour == date.Hour);
//        }

//        /// <summary>
//        /// Whether or not the date is the same minute.
//        /// </summary>
//        public static bool IsSameMinute(this DateTime source, DateTime date)
//        {
//            return (source.Year == date.Year &&
//                source.Month == date.Month &&
//                source.Day == date.Day &&
//                source.Hour == date.Hour &&
//                source.Minute == date.Minute);
//        }

//        /// <summary>
//        /// Whether or not the date is the same second.
//        /// </summary>
//        public static bool IsSameSecond(this DateTime source, DateTime date)
//        {
//            return (source.Year == date.Year &&
//                source.Month == date.Month &&
//                source.Day == date.Day &&
//                source.Hour == date.Hour &&
//                source.Minute == date.Minute &&
//                source.Second == date.Second);
//        }


//        # region WorkingDays

//        /// <summary>
//        /// If the date is not working day, returns the next date in the future
//        /// which is a working day.
//        /// </summary>
//        public static DateTime AdvanceToWorkingDay(this DateTime date)
//        {
//            while (!IsWorkingDay(date))
//            {
//                date = date.AddDays(1);
//            }
//            return date;
//        }

//        /// <summary>
//        /// Checks if the date passed is a working day.
//        /// </summary>
//        public static bool IsWorkingDay(this DateTime date)
//        {
//            if (date.DayOfWeek == DayOfWeek.Saturday ||
//                date.DayOfWeek == DayOfWeek.Sunday)
//                return false;

//            return !WorkingDays.IsPublicHoliday(date);
//        }

//        public static bool IsWorkingDay(this DateTime source, IEnumerable<DateTime> publicHolidays)
//        {
//            if (source.DayOfWeek == DayOfWeek.Saturday ||
//                source.DayOfWeek == DayOfWeek.Sunday)
//                return false;

//            return !source.IsSameDayAs(publicHolidays);
//        }
        
//        /// <summary>
//        /// Counts the working days between two dates.
//        /// </summary>
//        public static int CountWorkingDays(this DateTime date1, DateTime date2)
//        {
//            return WorkingDays.NumberOfDays(date1, date2);
//        }

//        /// <summary>
//        /// Adds number of working days to the date.
//        /// </summary>
//        public static DateTime TodayPlusWorkingDays(int workingDays, string time)
//        {
//            DateTime dateTime = DateTime.Now.AddWorkingDays(workingDays);
//            return dateTime.SetTime(time);
//        }

//        /// <summary>
//        /// Adds working days to the date.
//        /// </summary>
//        public static DateTime? RemoveTime(this DateTime? date)
//        {
//            if (date == null) { return null; }
//            return date.Value.RemoveTime();
//        }

//        public static DateTime RemoveTime(this DateTime date)
//        {
//            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
//        }

//        /// <summary>
//        /// Adds working days to the date.
//        /// </summary>
//        public static DateTime? SetTime(this DateTime? date, string time)
//        {
//            if (date == null) { return null; }
//            return date.Value.SetTime(time);
//        }

//        /// <summary>
//        /// Sets the time on the date.
//        /// </summary>
//        public static DateTime SetTime(this DateTime date, string time)
//        {
//            return (date.ToString("dd/MM/yyyy " + time).ToDate());
//        }

//        # endregion

//        /// <summary>
//        /// Returns the maximum date.
//        /// </summary>
//        public static DateTime MaxDate(this DateTime thisDate, DateTime date)
//        {
//            if (thisDate > date)
//                return thisDate;
//            return date;
//        }

//        #region Convert

//        /// <summary>
//        /// Converts the date into a string.
//        /// </summary>
//        public static string ToString(this DateTime? dateTime, string format)
//        {
//            if (!dateTime.HasValue)
//                return string.Empty;
//            else
//                return dateTime.Value.ToString(format);
//        }

//        /// <summary>
//        /// Converts the date into a string.
//        /// </summary>
//        public static string ToWeek(this DateTime dateTime, string format)
//        {
//            return ToWeek(dateTime, format, 0);
//        }

//        /// <summary>
//        /// Converts the date into a string.
//        /// </summary>
//        public static string ToWeek(this DateTime dateTime, string format, int offset)
//        {
//            return ToWeek(dateTime, System.Globalization.CultureInfo.CurrentCulture.Calendar, format, offset);
//        }

//        /// <summary>
//        /// Converts the date into a string.
//        /// </summary>
//        public static string ToWeek(this DateTime dateTime, System.Globalization.Calendar calendar, string format, int offset)
//        {
//            string week = (calendar.GetWeekOfYear(dateTime, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday) + offset).ToString();
//            string str = dateTime.ToString(format).Replace("w", "W");
//            return str.Replace("WW", week.PadLeft(2, (char)48)).Replace("W", week);
//        }

//        #endregion

//    }
//}
