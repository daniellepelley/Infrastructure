using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;

namespace Newton.Data
{
    /// <summary>
    /// A wrapper around a Linq To SQL data context
    /// </summary>
    public class LinqToSqlDataContext : IDataContext
    {
        #region Properties

        private DataContext dataContext;
        /// <summary>
        /// The wrapped Linq To SQL data context
        /// </summary>
        public DataContext DataContext
        {
            get { return dataContext; }
            set { dataContext = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A wrapper around a Linq To SQL data context
        /// </summary>
        public LinqToSqlDataContext(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Whether there are changes ready to be persisted to the data source
        /// </summary>
        public bool AreChanges()
        {
            var changeSet = dataContext.GetChangeSet();
            return
                changeSet.Deletes.Count > 0 ||
                changeSet.Inserts.Count > 0 ||
                changeSet.Updates.Count > 0;
        }

        /// <summary>
        /// Saves the changes to the data source
        /// </summary>
        public void SaveChanges()
        {
            dataContext.SubmitChanges();
        }

        #endregion
    }
}
