using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A repository that uses entity framework to comunicate with a database
    /// </summary>
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        #region Properties

        private ObjectSet<T> entitySet;

        private EntityFrameworkDataContext dataContext;
        /// <summary>
        /// The data context that interacts with the data source
        /// </summary>
        public IDataContext DataContext
        {
            get { return dataContext; }
        }

        /// <summary>
        /// The collection of items of type T in the base data
        /// </summary>
        public IQueryable<T> Items
        {
            get { return dataContext.ObjectContext.CreateObjectSet<T>(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A repository that uses entity framework to comunicate with a database
        /// </summary>
        public EntityFrameworkRepository(ObjectContext objectContext)
            : this(new EntityFrameworkDataContext(objectContext))
        { }

        /// <summary>
        /// A repository that uses entity framework to comunicate with a database
        /// </summary>
        public EntityFrameworkRepository(EntityFrameworkDataContext dataContext)
        {
            this.dataContext = dataContext;
            this.entitySet = this.dataContext.ObjectContext.CreateObjectSet<T>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queries the base data using the passed query
        /// </summary>
        public IQueryable<T> Query(IQuery query)
        {
            if (query is PredicateQuery<T>)
                return this.entitySet.Where(((PredicateQuery<T>)query).Predicate).AsQueryable();
            return null;
        }

        /// <summary>
        /// Saves the entity to the base data
        /// </summary>
        public void Save(T entity)
        {
            if (!this.Contains(entity))
                this.entitySet.Attach(entity);

            this.dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            if (!this.Contains(entity))
                this.entitySet.Attach(entity);

            this.dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            if (!this.Contains(entity))
                this.entitySet.AddObject(entity);

            this.dataContext.ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
        }

        public bool Contains(T item)
        {
            ObjectStateEntry state;
            if (!this.dataContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(item, out state))
                return false;
            return (state.State != EntityState.Detached);
        }

        #endregion
    }
}
