using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Newton.Dates
{
    /// <summary>
    /// Interface representing a period of time
    /// </summary>
    public interface  IPeriodOfTime
    {
        #region Properties

        /// <summary>
        /// The start of the period of time
        /// </summary>
        DateTime? Start { get; set; }

        /// <summary>
        /// The end of the period of time
        /// </summary>
        DateTime? End { get; set; }

        #endregion
    }
}
