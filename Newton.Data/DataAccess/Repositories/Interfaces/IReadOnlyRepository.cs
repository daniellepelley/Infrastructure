using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// An interface representing a repository
    /// </summary>
    public interface IReadOnlyRepository<T> where T : class
    {
        #region Properties

        /// <summary>
        /// The data context that interacts with the data source
        /// </summary>
        IDataContext DataContext { get; }

        /// <summary>
        /// The collection of items of type T in the base data
        /// </summary>
        IQueryable<T> Items { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Queries the base data using the passed query
        /// </summary>
        IQueryable<T> Query(IQuery query);

        #endregion
    }
}
