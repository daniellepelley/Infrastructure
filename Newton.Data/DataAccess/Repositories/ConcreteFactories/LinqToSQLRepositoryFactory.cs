using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Objects;

using Newton.Data;

namespace Newton.Data
{
    /// <summary>
    /// A factory which produces LinqToSQL repositories
    /// </summary>
    public class LinqToSQLRepositoryFactory : IRepositoryFactory
    {
        #region Properties

        private LinqToSqlDataContext dataContext;

        /// <summary>
        /// The contained DataContext used to comunicate with the database
        /// </summary>
        public LinqToSqlDataContext DataContext
        {
            get { return dataContext; }
            set { dataContext = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A factory which produces LinqToSQL repositories
        /// </summary>
        public LinqToSQLRepositoryFactory(DataContext dataContext)
            : this(new LinqToSqlDataContext(dataContext))
        { }


        /// <summary>
        /// A factory which produces LinqToSQL repositories
        /// </summary>
        public LinqToSQLRepositoryFactory(LinqToSqlDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a LinqToSQLRepository for type T
        /// </summary>
        public IRepository<T> Create<T>() where T : class
        {
            return new LinqToSQLRepository<T>(dataContext);
        }

        #endregion
    }
}