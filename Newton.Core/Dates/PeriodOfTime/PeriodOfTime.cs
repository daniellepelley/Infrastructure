using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Newton.Dates
{
    /// <summary>
    /// Class representing a period of time
    /// </summary>
    public class PeriodOfTime : IPeriodOfTime
    {
        #region Properties

        /// <summary>
        /// The start of the period of time
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// The end of the period of time
        /// </summary>
        public DateTime? End { get; set; }

        #endregion
    }
}
