using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Scheduling.Recursion
{
    /// <summary>
    /// Base class for recurrence
    /// </summary>
    public abstract class Recurrence
    {
        /// <summary>
        /// Returns a number of occurrences of the recurrence
        /// </summary>
        public abstract IEnumerable<DateTime> GetDates(DateTime startDate, int occurrences);

        /// <summary>
        /// Returns the recurrences between the two passed dates
        /// </summary>
        public abstract IEnumerable<DateTime> GetDates(DateTime startDate, DateTime endDate);
    }

    public class RecurrencePattern
    {
        private List<Recurrence> frequencies = new List<Recurrence>();

        public List<Recurrence> Frequencies
        {
            get { return frequencies; }
            set { frequencies = value; }
        }
    }

    public abstract class RecurrenceRange
    {

    }

    public class DateRecurrenceRange : RecurrenceRange
    {
        private DateTime endDate;
    }

    public class NumberRecurrenceRange : RecurrenceRange
    {
        private int number;
    }
}
