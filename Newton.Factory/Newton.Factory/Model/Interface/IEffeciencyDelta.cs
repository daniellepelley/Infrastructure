using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{

    public interface IEfficiencyDelta
        : IDelta
    {
        /// <summary>
        /// A delta
        /// </summary>
        double Delta { get; set; }

        /// <summary>
        /// The efficiency of the dimension including the delta of oppotunity realised
        /// </summary>
        double DeltaEfficiency(Rate conversionRate);
    }
}
