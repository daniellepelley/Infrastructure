using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Interface representing a potential and an actual speed
    /// </summary>
    public interface ILabourPotential
    {
        /// <summary>
        /// The actual speed of the process
        /// </summary>
        double ActualLabour { get; }

        /// <summary>
        /// The potential speed of the process
        /// </summary>
        double PotentialLabour { get; }
    }
}
