﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Validation;

namespace Newton.Data
{
    /// <summary>
    /// A decorator around a repository that makes validates entities before updating them
    /// </summary>
    public class ValidationRepository<T>
        : IRepository<T> where T : class
    {
        #region Properties

        private IRepository<T> repository;

        private IEntityRuleProvider<T> entityRuleProvider;

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
                return repository.Items;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A decorator around a repository that makes a security check before each transaction
        /// </summary>
        public ValidationRepository(IRepository<T> repository, IEntityRuleProvider<T> entityRuleProvider)
        {
            this.repository = repository;
            this.entityRuleProvider = entityRuleProvider;
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
            if (entityRuleProvider.IsValid(entity))
            {
                repository.Save(entity);
            }
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            repository.Delete(entity);
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            if (entityRuleProvider.IsValid(entity))
            {
                repository.Create(entity);
            }
        }

        #endregion
    }
}
