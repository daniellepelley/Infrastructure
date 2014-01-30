using System;

namespace Newton.Core
{
    /// <summary>
    /// An item used to build up an index measure
    /// </summary>
    public interface IIndexItem
    {
        /// <summary>
        /// The average
        /// </summary>
        double Average { get; set; }

        /// <summary>
        /// The weighting of the average
        /// </summary>
        double Weighting { get; set; }
    }
}
