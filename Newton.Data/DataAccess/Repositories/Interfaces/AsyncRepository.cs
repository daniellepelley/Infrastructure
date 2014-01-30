using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    public interface IAsyncRepository<T>
        where T : class
    {
        #region Properties

        /// <summary>
        /// The data context that interacts with the data source
        /// </summary>
        IDataContext DataContext { get; }

        /// <summary>
        /// The collection of items of type T in the base data
        /// </summary>
        IObservable<T> Items { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the entity to the base data
        /// </summary>
        void SaveAsync(T entity);

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        void DeleteAsync(T entity);

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        void CreateAsync(T entity);

        #endregion
    }
}
