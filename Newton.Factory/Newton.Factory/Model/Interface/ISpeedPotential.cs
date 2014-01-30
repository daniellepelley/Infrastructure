using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Interface representing a potential and an actual speed
    /// </summary>
    public interface ISpeedPotential
    {
        /// <summary>
        /// The actual speed of the process
        /// </summary>
        Rate ActualSpeed { get; }

        /// <summary>
        /// The potential speed of the process
        /// </summary>
        Rate PotentialSpeed { get; }
    }
}
