using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Interface representing a measure using a Rate
    /// </summary>
    public interface IRateMeasure
    {
        /// <summary>
        /// The title of the measure
        /// </summary>
        string Title { get; }

        /// <summary>
        /// The rate
        /// </summary>
        Rate Rate { get; }
    }
}