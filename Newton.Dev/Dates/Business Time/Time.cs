using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Extensions
{
    /// <summary>
    /// Struct represening a time.
    /// </summary>
    public struct Time
    {
        #region Parameters

        private DateTime dateTime;

        /// <summary>
        /// The hour part of the time.
        /// </summary>
        public int Hour
        {
            set
            {
                if (value >= 0 || value <= 23)
                {
                    dateTime = Convert.ToDateTime(string.Format(dateTime.ToString("dd/MM/yyyy {0}:mm"), value.ToString().PadLeft(2, (char)48)));
                }
            }
            get { return dateTime.Hour; }
        }

        /// <summary>
        /// The minute part of the time.
        /// </summary>
        public int Minute
        {
            set
            {
                if (value >= 0 || value <= 59)
                {
                    dateTime = Convert.ToDateTime(string.Format(dateTime.ToString("dd/MM/yyyy HH:{0}"), value.ToString().PadLeft(2, (char)48)));
                }
            }
            get { return dateTime.Minute; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Struct represening a time.
        /// </summary>
        public Time(int hour, int minute)
        {
            if (IsValidHour(hour)
                && IsValidMinute(minute))
            {
                dateTime = new DateTime(1, 1, 1, hour, minute, 0);

                //dateTime = Convert.ToDateTime(string.Format("01/01/0001 {0}:{1}", hour.ToString().PadLeft(2, (char)48), minute.ToString().PadLeft(2, (char)48)));
            }
            else
            {
                dateTime = new DateTime(1, 1, 1, 0, 0, 0);
                //dateTime = Convert.ToDateTime("01/01/0001 09:00");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To be phased out
        /// </summary>
        public double ToDouble()
        {
            return ToHours();
        }

        public double ToHours()
        {
            return ToHours(Hour, Minute);
        }

        public double ToMinutes()
        {
            return ToMinutes(Hour, Minute);
        }

        /// <summary>
        /// Check if the hour value is valid.
        /// </summary>
        public static bool IsValidHour(int hour)
        {
            return hour >= 0 && hour <= 23;
        }

        /// <summary>
        /// Check if the minute value is valid.
        /// </summary>
        public static bool IsValidMinute(int minute)
        {
            return minute >= 0 && minute <= 59;
        }

        public static DateTime SetTime(DateTime dateTime, Time time)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, time.Hour, time.Minute, 0);

            //return Convert.ToDateTime(string.Format(dateTime.ToString("dd/MM/yyyy {0}:{1}"), time.Hour.ToString().PadLeft(2, (char)48), time.Minute.ToString().PadLeft(2, (char)48)));
        }

        public static Time FromDate(DateTime dateTime)
        {
            return new Time(dateTime.Hour, dateTime.Minute);
        }

        /// <summary>
        /// Creates a time from a double
        /// </summary>
        public static Time FromDouble(double value)
        {
            Time time = new Time();
            time.Hour = (int)value;
            time.Minute = (int)((value - time.Hour) * 60);
            return time;
        }

        /// <summary>
        /// Creates a time from a double
        /// </summary>
        public static double ToHours(int hour, int minute)
        {
            return (double)hour + ((double)minute / 60);
        }

        /// <summary>
        /// Creates a time from a double
        /// </summary>
        public static double ToMinutes(int hour, int minute)
        {
            return (double)hour * 60 + ((double)minute);
        }

        #endregion

        #region Operators

        public static bool operator <(Time time1, Time time2)
        {
            return (time1.dateTime < time2.dateTime);
        }

        public static bool operator <=(Time time1, Time time2)
        {
            return (time1.dateTime <= time2.dateTime);
        }

        public static bool operator >(Time time1, Time time2)
        {
            return (time1.dateTime > time2.dateTime);
        }

        public static bool operator >=(Time time1, Time time2)
        {
            return (time1.dateTime >= time2.dateTime);
        }

        public static bool operator ==(Time time1, Time time2)
        {
            return (time1.dateTime == time2.dateTime);
        }

        public static bool operator !=(Time time1, Time time2)
        {
            return (time1.dateTime != time2.dateTime);
        }

        #endregion
    }

}