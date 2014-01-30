using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// A constraint dimension
    /// </summary>
    public interface IConstraintDimension
    {
        #region Properties

        /// <summary>
        /// The rate in units
        /// </summary>
        Rate UnitRate(Rate conversionRate);

        /// <summary>
        /// The timespan rate
        /// </summary>
        TimeSpanRate TimeSpanRate(Rate conversionRate);

        /// <summary>
        /// The efficiency
        /// </summary>
        double Efficiency(Rate conversionRate);

        #endregion
    }
}
