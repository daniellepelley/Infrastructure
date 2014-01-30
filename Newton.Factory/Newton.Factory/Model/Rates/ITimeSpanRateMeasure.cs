using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Interface representing a measure using a TimeSpanRate
    /// </summary>
    public interface ITimeSpanRateMeasure
    {
        /// <summary>
        /// The title of the measure
        /// </summary>
        string Title { get; }

        /// <summary>
        /// The timespan rate
        /// </summary>
        TimeSpanRate Rate { get; }
    }
}