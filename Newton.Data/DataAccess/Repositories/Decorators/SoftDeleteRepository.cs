using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A decorator around a repository that forces soft delete of records
    /// </summary>
    public class SoftDeleteRepository<T>
        : IRepository<T> where T : class
    {
        #region Properties

        private IRepository<T> repository;

        private System.Linq.Expressions.Expression<Func<T, bool>> isLiveFilter;

        private System.Linq.Expressions.Expression<Action<T>> markAsDeleted;

        /// <summary>
        /// The data context that interacts with the data source
        /// </summary>
        public IDataContext DataContext
        {
            get { return repository.DataContext; }
        }

        /// <summary>
        /// The collection of items of type T in the base data
        /// </summary>
        public IQueryable<T> Items
        {
            get
            {
                return repository.Items.Where(isLiveFilter);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A decorator around a repository that makes a security check before each transaction
        /// </summary>
        public SoftDeleteRepository(IRepository<T> repository, System.Linq.Expressions.Expression<Func<T, bool>> isLiveFilter, System.Linq.Expressions.Expression<Action<T>> markAsDeleted)
        {
            this.repository = repository;
            this.isLiveFilter = isLiveFilter;
            this.markAsDeleted = markAsDeleted;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queries the base data using the passed query
        /// </summary>
        public IQueryable<T> Query(IQuery query)
        {
            return repository.Query(query);
        }

        /// <summary>
        /// Saves the entity to the base data
        /// </summary>
        public void Save(T entity)
        {
            repository.Save(entity);
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            markAsDeleted.Compile().Invoke(entity);
            repository.Save(entity);
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            repository.Create(entity);
        }

        #endregion
    }
}
