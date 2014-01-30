using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Objects;

namespace Newton.Data
{
    /// <summary>
    /// A factory which produces Entity Framework repositories
    /// </summary>
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        #region Properties

        private ObjectContext objectContext;

        /// <summary>
        /// The contained ObjectContext used to comunicate with the database
        /// </summary>
        public ObjectContext ObjectContext
        {
            get { return objectContext; }
            set { objectContext = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A factory which produces Entity Framework repositories
        /// </summary>
        public EntityFrameworkRepositoryFactory(ObjectContext objectContext)
        {
            this.objectContext = objectContext;
        }

        /// <summary>
        /// A factory which produces Entity Framework repositories
        /// </summary>
        public EntityFrameworkRepositoryFactory(string connectionString)
        {
            this.objectContext = new System.Data.Objects.ObjectContext(connectionString);
        }


        #endregion

        #region Methods

        /// <summary>
        /// Creates a EntityFrameworkRepository for type T
        /// </summary>
        public IRepository<T> Create<T>() where T : class
        {
            return new EntityFrameworkRepository<T>(objectContext);
        }

        #endregion
    }
}