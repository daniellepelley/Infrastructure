using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A decorator around a repository which injects logging
    /// </summary>
    public class LoggableRepository<T>
        : IRepository<T> where T : class
    {
        #region Properties

        private IRepository<T> repository;

        private ILogger<T> logger;

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
        /// A decorator around a repository which injects logging
        /// </summary>
        public LoggableRepository(IRepository<T> repository, ILogger<T> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves the entity to the base data
        /// </summary>
        public void Save(T entity)
        {
            logger.Log(entity);
            repository.Save(entity);
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            logger.Log(entity);
            repository.Delete(entity);
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            logger.Log(entity);
            repository.Create(entity);
        }

        #endregion
    }
}
