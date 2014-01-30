using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Data;
using Newton.Testing;

namespace Newton.Data.Testing
{
    /// <summary>
    /// A Mock repository with auto generated data
    /// </summary>
    public class MockRepository<T> : IRepository<T> where T : class
    {
        #region Properties

        private Populator populator = new Populator();
        private List<T> list = new List<T>();
        
        public IQueryable<T> Items
        {
            get { return list.AsQueryable(); }
        }

        public IDataContext DataContext
        {
            get { return null; }
        }

        #endregion

        #region Constructors

        public MockRepository()
            : this(20) { }

        public MockRepository(int numberOfRecords)
        {
            for (int i = 0; i < numberOfRecords; i++)
                list.Add(populator.Create<T>());
        }

        #endregion

        #region Methods
        
        public IQueryable<T> Query(IQuery query)
        {
            return list.AsQueryable<T>();        
        }

        public void Save(T entity)
        {
            
        }

        public void Delete(T entity)
        {
            list.Remove(entity);
        }

        public void Create(T entity)
        {
            list.Add(entity);
        }

        #endregion
    }
}
