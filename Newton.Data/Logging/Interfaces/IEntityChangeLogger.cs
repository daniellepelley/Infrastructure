using System;
using System.Collections.Generic;

namespace Newton.Data
{
    /// <summary>
    /// Creates a log of a change on an entity
    /// </summary>
    public interface IEntityChangeLogger
    {
        #region Properties

        /// <summary>
        /// The data context that contains the entities to be logged
        /// </summary>
        IDataContext DataContext { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a log of all the changes on the passed entity
        /// </summary>
        IEnumerable<IEntityChangeLogItem> CreateChangeLog(object modifiedEntity);

        /// <summary>
        /// Creates a log of all the changes on the passed entity
        /// </summary>
        IEnumerable<IEntityChangeLogItem> CreateChangeLog<T>(T modifiedEntity)
            where T : class;

        #endregion
    }
}