using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Validation;

namespace Newton.Data
{
    /// <summary>
    /// A decorator around a repository
    /// </summary>
    public class RepositoryDecoratorBase<T>
        : IRepository<T> where T : class
    {
        #region Properties

        protected IRepository<T> repository;

        /// <summary>
        /// The data context that interacts with the data source
        /// </summary>
        public virtual IDataContext DataContext
        {
            get { return repository.DataContext; }
        }

        /// <summary>
        /// The collection of items of type T in the base data
        /// </summary>
        public virtual IQueryable<T> Items
        {
            get
            {
                return repository.Items;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A decorator around a repository
        /// </summary>
        public RepositoryDecoratorBase(IRepository<T> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the entity to the base data
        /// </summary>
        public virtual void Save(T entity)
        {
            repository.Save(entity);
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public virtual void Delete(T entity)
        {
            repository.Delete(entity);
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public virtual void Create(T entity)
        {
            repository.Create(entity);
        }

        #endregion
    }
}
