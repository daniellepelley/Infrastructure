using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Testing
{
    /// <summary>
    /// Interface representing an object that creates random data for a property
    /// </summary>
    public interface IFieldPopulator
    {
        /// <summary>
        /// Creates a property containing random data
        /// </summary>
        object Create(Random random);

        /// <summary>
        /// Checks whether the populator works on the passed type 
        /// </summary>
        bool CheckType(Type type);
    }
}
