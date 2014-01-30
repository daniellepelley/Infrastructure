using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// A constraint dimension measured in time
    /// </summary>
    public class TimespanDimension : IConstraintDimension
    {
        #region Properties

        private TimeSpanRate rate;
        /// <summary>
        /// The timespan rate
        /// </summary>
        public TimeSpanRate Rate
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
                    Quantity = rate.ToRate(conversionRate).GetQuantityForTimeSpan(conversionRate.TimeSpan),
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
                    Quantity = rate.GetQuantityForTimeSpan(conversionRate.TimeSpan),
                    TimeSpan = conversionRate.TimeSpan
                };
        }

        /// <summary>
        /// The efficiency of the dimension
        /// </summary>
        public double Efficiency(Rate conversionRate)
        {
            return rate.Efficiency;
        }

        #endregion
    }
}
