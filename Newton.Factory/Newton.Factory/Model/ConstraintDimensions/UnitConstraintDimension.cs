using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// A constraint dimension measured in units
    /// </summary>
    public class UnitDimension : IConstraintDimension
    {
        #region Properties

        private Rate rate;
        /// <summary>
        /// The timespan rate
        /// </summary>
        public virtual Rate Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        /// <summary>
        /// The rate in units
        /// </summary>
        public Rate UnitRate(Rate conversionRate)
        {
            return
                new Rate()
                {
                    Quantity = Rate.GetQuantityForTimeSpan(conversionRate.TimeSpan),
                    TimeSpan = conversionRate.TimeSpan
                };
        }

        /// <summary>
        /// The timespan rate
        /// </summary>
        public TimeSpanRate TimeSpanRate(Rate conversionRate)
        {
            return
                new TimeSpanRate()
                {
                    Quantity = Rate.ToTimeSpanRate(conversionRate).GetQuantityForTimeSpan(conversionRate.TimeSpan),
                    TimeSpan = conversionRate.TimeSpan
                };
        }

        /// <summary>
        /// The efficiency of the dimension
        /// </summary>
        public double Efficiency(Rate conversionRate)
        {
            return TimeSpanRate(conversionRate).Efficiency;
        }

        #endregion
    }
}
