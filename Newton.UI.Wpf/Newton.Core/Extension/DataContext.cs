using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Pluto.Extensions
{
    public static class DataContextExtensions
    {
        /// <summary>
        /// Checks whether are not there are changes to the entities
        /// within the data context
        /// </summary>
        public static bool AreChanges(this DataContext dataConext)
        {
            ChangeSet changeSet = dataConext.GetChangeSet();
            return (changeSet.Deletes.Count > 0 ||
                changeSet.Inserts.Count > 0 ||
                changeSet.Updates.Count > 0);
        }
    }
}
