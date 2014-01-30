using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns whether the passed character isn't a letter, number or
        /// white space.
        /// </summary>
        public static TimeSpan Multiple(this TimeSpan source, double number)
        {
            return new TimeSpan((long)((source.TotalMilliseconds * 10000) * number));
        }

        /// <summary>
        /// Returns whether the passed character isn't a letter, number or
        /// white space.
        /// </summary>
        public static TimeSpan Divide(this TimeSpan source, double number)
        {
            return new TimeSpan((long)((source.TotalMilliseconds * 10000) / number));
        }
    }
}
