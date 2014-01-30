using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A decorator around a repository that instantly resists on Save or Delete
    /// </summary>
    public class InstantPersistanceRepository<T>
        : IRepository<T> where T : class
    {
        #region Properties

        private IRepository<T> repository;

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
            get { return repository.Items; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A decorator around a repository that instantly resists on Save or Delete
        /// </summary>
        public InstantPersistanceRepository(IRepository<T> repository)
        {
            this.repository = repository;
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
            DataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            repository.Delete(entity);
            DataContext.SaveChanges();
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            repository.Create(entity);
            DataContext.SaveChanges();
        }



        #endregion
    }
}
