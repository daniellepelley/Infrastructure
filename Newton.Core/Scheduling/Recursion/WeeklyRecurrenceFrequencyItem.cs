using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Scheduling.Recursion
{
    /// <summary>
    /// A class which represent a single pattern of weekly recurrence of dates
    /// </summary>
    public class WeeklyRecurrenceFrequencyItem : Recurrence
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

        private List<DayOfWeek> dayOfWeeks = new List<DayOfWeek>();
        /// <summary>
        /// The type of day to include (All, WeekDays, WorkingDays)
        /// </summary>
        public List<DayOfWeek> DayOfWeeks
        {
            get { return dayOfWeeks; }
            set { dayOfWeeks = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public WeeklyRecurrenceFrequencyItem()
        { }

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public WeeklyRecurrenceFrequencyItem(IEnumerable<DayOfWeek> dayOfWeeks)
        {
            this.dayOfWeeks.AddRange(dayOfWeeks.OrderBy(d => d));
        }

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public WeeklyRecurrenceFrequencyItem(int frequency)
        {
            this.frequency = frequency;
        }

        /// <summary>
        /// A class which creates a daily recurrence of dates
        /// </summary>
        public WeeklyRecurrenceFrequencyItem(IEnumerable<DayOfWeek> dayOfWeeks, int numberOfWeeks)
            : this(dayOfWeeks)
        {
            this.frequency = numberOfWeeks;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all dates that fit the recurrence between the two dates
        /// </summary>
        public override IEnumerable<DateTime> GetDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> output = new List<DateTime>();

            int numberOfWeeks = 0;
            while (true)
            {
                foreach (var daysOfWeek in dayOfWeeks)
                {
                    int daysToAdd = (daysOfWeek - startDate.DayOfWeek) + ((numberOfWeeks * this.frequency) * 7);

                    if (daysToAdd >= 0)
                    {
                        DateTime newDate = startDate.AddDays(daysToAdd);

                        if (newDate <= endDate)
                            output.Add(newDate);
                        else
                            return output;
                    }
                }
                numberOfWeeks++;
            }
        }

        /// <summary>
        /// Gets all dates that fit the recurrence between the two dates
        /// </summary>
        public override IEnumerable<DateTime> GetDates(DateTime startDate, int occurrences)
        {
            List<DateTime> output = new List<DateTime>();

            int[] days = dayOfWeeks.OrderBy(d => d).Select(d => (int)d).ToArray();
            int startDay = (int)startDate.DayOfWeek;

            int numberOfWeeks = 0;
            while (true)
            {
                foreach (var daysOfWeek in days)
                {
                    int daysToAdd = (daysOfWeek - startDay) + ((numberOfWeeks * this.frequency) * 7);

                    if (daysToAdd >= 0)
                    {
                        output.Add(startDate.AddDays(daysToAdd));

                        if (output.Count >= occurrences)
                            return output;
                    }
                }
                numberOfWeeks++;
            }
        }

        #endregion
    }

}