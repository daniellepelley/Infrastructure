using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which states that text must be in the format HH:MM
    /// </summary>
    public class Time24HourRule : RegexRule
    {
        #region Constructors

        /// <summary>
        /// A rule which states that text must be in the format HH:MM
        /// </summary>
        public Time24HourRule()
            : base("^([0-1][0-9]|2[0-3]):[0-5][0-9]$", "Must be in the format HH:MM")
        { }

        #endregion
    }
}