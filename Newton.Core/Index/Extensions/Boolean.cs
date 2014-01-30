using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    /// <summary>
    /// Class for managing booleans.
    /// </summary>
    public static class Boolean
    {
        /// <summary>
        /// Returns whether the bool? has a value of true.
        /// </summary>
        public static bool IsTrue(this bool? boolean)
        {
            if (boolean.HasValue)
                return boolean.Value;
            return false;
        }

        /// <summary>
        /// Returns whether the bool? has a value of false.
        /// </summary>
        public static bool IsFalse(this bool? boolean)
        {
            if (boolean.HasValue)
                return !boolean.Value;
            return false;
        }
    }
}
