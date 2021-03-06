﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A factory that creates a decorator around a repository that instantly persists on Save or Delete
    /// </summary>
    public class InstantPersistanceRepositoryFactory
        : IRepositoryFactory
    {
        #region Properties

        private IRepositoryFactory repositoryFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// A factory that creates a decorator around a repository that instantly persists on Save or Delete
        /// </summary>
        public InstantPersistanceRepositoryFactory(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a repository that instantly persists on Save or Delete
        /// </summary>
        public IRepository<T> Create<T>() where T : class
        {
            return new InstantPersistanceRepository<T>(repositoryFactory.Create<T>());
        }

        #endregion
    }
}
