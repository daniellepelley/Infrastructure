using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Scheduling.Recursion
{
    public enum DayType { All, WeekDays, BusinessDays };

    /// <summary>
    /// A class which creates a daily recurrence of dates
    /// </summary>
    public class DailyRecurrence : Recurrence
    {
        #region Properties

        private int frequency = 1;
        /// <summary>
        /// The numbers of weeks between each interval
        /// </summary>
        public int Frequency
        {
            get { return frequency; }
            set
            {
                if (value < 1)
                    frequency = 1;
                else
                    frequency = value;
            }
        }

        private DayType dayType;
        /// <summary>
        /// The type of day to include (All, WeekDays, BusinessDays)
        /// </summary>
        public DayType DayType
        {
            get { return dayType; }
            set { dayType = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public DailyRecurrence()
        { }

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public DailyRecurrence(DayType dayType)
        {
            this.dayType = dayType;
        }

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public DailyRecurrence(int frequency)
        {
            this.frequency = frequency;
        }

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public DailyRecurrence(DayType dayType, int frequency)
            : this(dayType)
        {
            this.frequency = frequency;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all dates that fit the recurrence between the two dates
        /// </summary>
        public override IEnumerable<DateTime> GetDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> list = new List<DateTime>(new DateTime[] { GetFirstDate(startDate) });
            while (list.LastOrDefault() < endDate)
            {
                list.Add(GetNextDate(list.LastOrDefault()));
            }
            return list;
        }

        /// <summary>
        /// Gets all dates that fit the recurrence between the two dates
        /// </summary>
        public override IEnumerable<DateTime> GetDates(DateTime startDate, int occurrences)
        {
            List<DateTime> list = new List<DateTime>(new DateTime[] { GetFirstDate(startDate) });
            for (int i = 0; i < occurrences - 1; i++)
            {
                list.Add(GetNextDate(list.LastOrDefault()));
            }

            return list;
        }

        private DateTime GetFirstDate(DateTime dateTime)
        {
            if (dayType == Recursion.DayType.All)
            {
                return dateTime;
            }
            else if (dayType == Recursion.DayType.WeekDays &&
                dateTime.IsBusinessDay())
            {
                return dateTime;
            }
            else if (dayType == Recursion.DayType.BusinessDays &&
                dateTime.IsBusinessDay())
            {
                return dateTime;
            }

            return GetNextDate(dateTime);
        }

        private DateTime GetNextDate(DateTime dateTime)
        {
            if (dayType == Recursion.DayType.WeekDays)
            {
                return dateTime.AddWeekDays(frequency);
            }
            else if (dayType == Recursion.DayType.BusinessDays)
            {
                return dateTime.AddBusinessDays(frequency);
            }
            else
            {
                return dateTime.AddDays(frequency);
            }
        }

        #endregion
    }
}