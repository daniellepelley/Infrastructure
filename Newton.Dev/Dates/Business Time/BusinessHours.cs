using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Extensions
{
    /// <summary>
    /// Class representing times within business hours.
    /// </summary>
    public class BusinessHours
    {
        #region Parameters

        private Time startTime;
        /// <summary>
        /// Start time for working hours.
        /// </summary>
        public Time StartTime
        {
            set { startTime = value; }
            get { return startTime; }
        }

        private Time endTime;
        /// <summary>
        /// End time for working hours.
        /// </summary>
        public Time EndTime
        {
            set { endTime = value; }
            get { return endTime; }
        }

        /// <summary>
        /// Returns the number of minutes in a working day
        /// </summary>
        public double MinutesInADay
        {
            get { return endTime.ToMinutes() - startTime.ToMinutes(); }
        }

        #endregion

        #region Constructors

        private BusinessHours(Time startTime, Time endTime)
        {
            if (startTime > endTime)
            {
                throw (new Exception("Start Time can't be more than End Time"));
            }

            this.startTime = startTime;
            this.endTime = endTime;
        }

        /// <summary>
        /// Class representing times within working hours.
        /// </summary>
        /// <remarks>
        /// Default is 08:00 to 17:00
        /// </remarks>
        public static BusinessHours Construct()
        {
            return Construct(8, 0, 17, 0);
        }

        /// <summary>
        /// Class representing times within working hours.
        /// </summary>
        public static BusinessHours Construct(Time startTime, Time endTime)
        {
            if (startTime == null ||
                endTime == null ||
                startTime > endTime)
            {
                return null;
            }

            return new BusinessHours(startTime, endTime);
        }

        /// <summary>
        /// Class representing times within working hours.
        /// </summary>
        public static BusinessHours Construct(int startHour, int startMinute, int endHour, int endMinute)
        {
            return Construct(new Time(startHour, startMinute), new Time(endHour, endMinute));
        }

        /// <summary>
        /// Class representing times within working hours.
        /// </summary>
        public static BusinessHours Construct(string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(string.Format("01/01/0001 {0}", startDate));
            DateTime end = DateTime.Parse(string.Format("01/01/0001 {0}", endDate));

            return Construct(new Time(start.Hour, start.Minute), new Time(end.Hour, end.Minute));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the datetime is within working hours
        /// </summary>
        public bool IsWithinWorkingHours(DateTime? dateTime)
        {
            if (dateTime == null)
                return false;
            return IsWithinWorkingHours(dateTime.Value);
        }

        /// <summary>
        /// Checks if the datetime is within working hours
        /// </summary>
        public bool IsWithinWorkingHours(DateTime dateTime)
        {
            Time checkTime = Time.FromDate(dateTime);
            return checkTime >= startTime &&
                checkTime <= endTime;
        }

        /// <summary>
        /// Returns the next valid date time with the working hours
        /// </summary>
        public DateTime? NextValidDateTime(DateTime? dateTime)
        {
            return NextValidDateTime(dateTime, 0);
        }

        /// <summary>
        /// Returns the next valid date time with the working hours
        /// </summary>
        public DateTime? NextValidDateTime(DateTime? dateTime, int bufferInMinutes)
        {
            if (dateTime == null)
                return dateTime;
            return NextValidDateTime(dateTime.Value, bufferInMinutes);
        }

        /// <summary>
        /// Returns the next valid date time with the working hours
        /// </summary>
        public DateTime NextValidDateTime(DateTime dateTime)
        {
            return NextValidDateTime(dateTime, 0);
        }

        /// <summary>
        /// Returns the next valid date time with the working hours
        /// </summary>
        public DateTime NextValidDateTime(DateTime dateTime, int bufferInMinutes)
        {
            Time checkTime = Time.FromDate(dateTime);
            if (checkTime < startTime)
                return Time.SetTime(dateTime, startTime);
            else if (checkTime > endTime)
                return Time.SetTime(dateTime.AddDays(1), endTime);
            else
                return dateTime;
        }

        /// <summary>
        /// Auto corrects the datetime so it is within working hours
        /// </summary>
        public DateTime? AutoCorrectTime(DateTime? dateTime)
        {
            return AutoCorrectTime(dateTime, 0);
        }

        /// <summary>
        /// Auto corrects the datetime so it is within working hours
        /// </summary>
        public DateTime? AutoCorrectTime(DateTime? dateTime, int bufferInMinutes)
        {
            if (dateTime == null)
                return dateTime;
            return AutoCorrectTime(dateTime.Value, bufferInMinutes);
        }

        /// <summary>
        /// Auto corrects the datetime so it is within working hours
        /// </summary>
        public DateTime AutoCorrectTime(DateTime dateTime)
        {
            return AutoCorrectTime(dateTime, 0);
        }

        /// <summary>
        /// Auto corrects the datetime so it is within working hours
        /// </summary>
        public DateTime AutoCorrectTime(DateTime dateTime, int bufferInMinutes)
        {
            Time checkTime = Time.FromDate(dateTime);
            if (checkTime < startTime)
                return Time.SetTime(dateTime, startTime);
            else if (checkTime > endTime)
                return Time.SetTime(dateTime, endTime);
            else
                return dateTime;
        }

        public double GetTotalHours(DateTime dateTime1, DateTime dateTime2)
        {
            double partDayHours = 0;
            int minusDays = 0;

            if (this.IsWithinWorkingHours(dateTime1) &&
                dateTime1.IsBusinessDay())
            {
                minusDays++;
                double time1 = Time.ToHours(dateTime1.Hour, dateTime1.Minute);
                double endTimeDouble = endTime.ToDouble();

                partDayHours += endTimeDouble - time1;
            }

            if (this.IsWithinWorkingHours(dateTime2) &&
                dateTime2.IsBusinessDay())
            {
                minusDays++;
                double time2 = Time.ToHours(dateTime2.Hour, dateTime2.Minute);
                double startTimeDouble = startTime.ToDouble();

                partDayHours += time2 - startTimeDouble;
            }

            int workingDays = BusinessDays.NumberOfDays(dateTime1, dateTime2) - minusDays;

            return (workingDays * (endTime.ToDouble() - startTime.ToDouble())) +
                partDayHours;
        }

        public double GetTotalMinutes(DateTime dateTime1, DateTime dateTime2)
        {
            double partDayMinutes = 0;
            int minusDays = 0;

            if (this.IsWithinWorkingHours(dateTime1) &&
                dateTime1.IsBusinessDay())
            {
                minusDays++;
                double time1 = Time.ToMinutes(dateTime1.Hour, dateTime1.Minute);
                double endTimeDouble = endTime.ToMinutes();

                partDayMinutes += (endTimeDouble - time1);
            }

            if (this.IsWithinWorkingHours(dateTime2) &&
                dateTime2.IsBusinessDay())
            {
                minusDays++;
                double time2 = Time.ToMinutes(dateTime2.Hour, dateTime2.Minute);
                double startTimeDouble = startTime.ToMinutes();

                partDayMinutes += time2 - startTimeDouble;
            }

            int workingDays = BusinessDays.NumberOfDays(dateTime1, dateTime2) - minusDays;

            return (workingDays * (endTime.ToMinutes() - startTime.ToMinutes())) +
                partDayMinutes;
        }

        /// <summary>
        /// Sets the start time on the passed date
        /// </summary>
        public DateTime GetStartDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, startTime.Hour, startTime.Minute, 0);
        }

        /// <summary>
        /// Sets the end time on the passed date
        /// </summary>
        public DateTime GetEndDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, endTime.Hour, endTime.Minute, 0);
        }

        public DateTime AddMinutes(DateTime dateTime, int numberOfMinutes)
        {
            //Whole number of days
            int numberOfDays = (int)(numberOfMinutes / MinutesInADay);

            //The remaining minutes
            double remainingMinutesToAdd = numberOfMinutes - (numberOfDays * MinutesInADay);

            //Calculates the number of minutes until the end time for the passed datetime
            double minutesLeftInDay = endTime.ToMinutes() - Time.FromDate(dateTime).ToMinutes();
            minutesLeftInDay = (minutesLeftInDay <= 0 ? 0 : minutesLeftInDay);

            if (minutesLeftInDay <= remainingMinutesToAdd)
                numberOfDays++;

            if (remainingMinutesToAdd < minutesLeftInDay)
            {
                return dateTime.AddBusinessDays(numberOfDays).AddMinutes(remainingMinutesToAdd);
            }
            else
            {
                return Time.SetTime(dateTime, startTime).AddBusinessDays(numberOfDays).AddMinutes(remainingMinutesToAdd - minutesLeftInDay);
            }
        }


        #endregion
    }
}