using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Data;

namespace Newton.UI.Wpf
{
    public class CRUDService<T> : IRepository<T>
        where T : class
    {
        private IRepository<T> repository;

        public IRepository<T> Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        public bool AreChanges()
        {
            return repository.DataContext.AreChanges();
        }

        public IQueryable<T> Query(IQuery query)
        {
            return repository.Query(query);
        }

        public void Save(T entity)
        {
            repository.Save(entity);
        }

        public void Delete(T entity)
        {
            repository.Delete(entity);
        }

        public void Create(T entity)
        {
            repository.Create(entity);
        }

        public IQueryable<T> Items
        {
            get { return repository.Items; }
        }

        public CRUDService()
        { }

        public CRUDService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public IDataContext DataContext
        {
            get { return repository.DataContext; }
        }
    }

    public class CRUDServiceFactory
    {
        public CRUDService<T> Create<T>() where T : class
        {
            return new CRUDService<T>();
        }
    }
}
