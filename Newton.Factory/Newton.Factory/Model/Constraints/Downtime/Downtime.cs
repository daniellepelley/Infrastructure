using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Downtime as a timespan dimension
    /// </summary>
    public class Downtime
        : TimespanDimension,
        IDowntime,
        IEfficiencyDelta
    {
        #region Properties

        private string title;
        /// <summary>
        /// The title of the downtime
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private double delta;
        /// <summary>
        /// The delta
        /// </summary>
        public double Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The efficiency of the dimension including the delta of oppotunity realised
        /// </summary>
        public double DeltaEfficiency(Rate conversionRate)
        {
            double efficiency = this.Efficiency(conversionRate);
            return (delta * (1 - efficiency)) + efficiency;
        }

        #endregion
    }
}
