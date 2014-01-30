using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Interface representing a data context
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Checks if there are changes to be persited
        /// </summary>
        bool AreChanges();

        /// <summary>
        /// Saves the changes to the data source
        /// </summary>
        void SaveChanges();
    }
}
