using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;


namespace Newton.Data
{    
    public class LinqToSQLRepository<T> : IRepository<T>
        where T : class
    {
        #region Properties

        private LinqToSqlDataContext dataContext;

        public IDataContext DataContext
        {
            get { return dataContext; }
        }

        public IQueryable<T> Items
        {
            get { return dataContext.DataContext.GetTable<T>().AsQueryable(); }
        }

        #endregion

        #region Constructors

        public LinqToSQLRepository(DataContext dataContext)
            : this(new LinqToSqlDataContext(dataContext))
        { }

        public LinqToSQLRepository(LinqToSqlDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queries the base data using the passed query
        /// </summary>
        public IQueryable<T> Query(IQuery query)
        {
            if (query is PredicateQuery<T>)
                return dataContext.DataContext.GetTable<T>().Where(((PredicateQuery<T>)query).Predicate).AsQueryable();
            return null;
        }

        /// <summary>
        /// Saves the entity to the base data
        /// </summary>
        public void Save(T entity)
        {
            dataContext.DataContext.GetTable<T>().Attach(entity);
            dataContext.DataContext.Refresh(RefreshMode.KeepCurrentValues, entity);
        }

        /// <summary>
        /// Deletes the entity from the base data
        /// </summary>
        public void Delete(T entity)
        {
            dataContext.DataContext.GetTable<T>().DeleteOnSubmit(entity);
        }

        /// <summary>
        /// Creates a new entity of type T in the base data
        /// </summary>
        public void Create(T entity)
        {
            dataContext.DataContext.GetTable<T>().InsertOnSubmit(entity);
        }

        #endregion
    }

    //public class LinqToSQLRepository : IRepository<object>
    //{
    //    private DataContext dataContext;

    //    public DataContext DataContext
    //    {
    //        get { return dataContext; }
    //    }

    //    private Type type;

    //    public Type Type
    //    {
    //        get { return type; }
    //        set { type = value; }
    //    }

    //    public LinqToSQLRepository(DataContext dataContext, Type type)
    //    {
    //        this.dataContext = dataContext;
    //        this.type = type;
    //    }

    //    public IEnumerable<object> Find(Func<object, bool> whereFunction)
    //    {
    //        return dataContext.GetTable<object>().Where(whereFunction);
    //    }

    //    public void Save(object entity)
    //    {
    //        dataContext.GetTable(type).InsertOnSubmit(entity);
    //        dataContext.SubmitChanges();
    //    }

    //    public void Delete(object entity)
    //    {
    //        dataContext.GetTable(type).DeleteOnSubmit(entity);
    //        dataContext.SubmitChanges();
    //    }

    //    public IEnumerable<object> Items
    //    {
    //        get { return (IEnumerable<object>)dataContext.GetTable(type).AsQueryable(); }
    //    }

    //    public IEnumerable<object> GetAll()
    //    {
    //        return Items;
    //    }

    //    IDataContext IRepository<object>.DataContext
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    IQueryable<object> IRepository<object>.Items
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public IQueryable<object> Query(IQuery<object> query)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <summary>
    //    /// Creates a new entity of type T in the base data
    //    /// </summary>
    //    public void Create(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
