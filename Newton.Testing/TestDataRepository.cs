using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Data;

namespace Newton.Testing
{
    public class TestDataRepository<T> : IRepository<T> where T : class
    {
        private int numberOfRecords;
        /// <summary>
        /// The number of records to display
        /// </summary>
        public int NumberOfRecords
        {
            get { return numberOfRecords; }
            set { numberOfRecords = value; }
        }

        private Populator populator = new Populator();

        public IQueryable<T> Items
        {
            get
            {
                List<T> list = new List<T>();
                for (int i = 0; i < numberOfRecords; i++)
                {
                    list.Add(populator.Create<T>());
                }
                return list.AsQueryable();
            }
        }

        public IQueryable<T> Query(IQuery query)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < new Random(DateTime.Now.Millisecond).Next(numberOfRecords); i++)
            {
                list.Add(populator.Create<T>());
            }
            return list.AsQueryable();
        }

        public void Save(T entity)
        { }

        public void Delete(T entity)
        { }

        public void Create(T entity)
        { }

        public IDataContext DataContext
        {
            get { return null; }
        }
    }


    public class TestDataRepositoryFactory : IRepositoryFactory
    {
        private int numberOfRecords = 4;

        public TestDataRepositoryFactory()
        { }

        //public TestDataRepositoryFactory(int numberOfRecords = 20)
        //{
        //    this.numberOfRecords = numberOfRecords;
        //}

        public IRepository<T> Create<T>() where T : class
        {
            return new TestDataRepository<T>() { NumberOfRecords = numberOfRecords };
        }
    }
}
