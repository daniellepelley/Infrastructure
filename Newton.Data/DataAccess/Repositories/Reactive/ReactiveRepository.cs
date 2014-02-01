using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Newton.Data
{
    public class ReactiveRepository<T> : IAsyncRepository<T> where T : class
    {
        #region Properties

        private IRepository<T> repository;

        public IDataContext DataContext
        {
            get { return repository.DataContext; }
        }

        public IObservable<T> Items
        {
            get { return items.ToObservable(); }
        }

        private IEnumerable<T> items
        {
            get
            {
                foreach (var item in repository.Items)
                {
                    System.Threading.Thread.Sleep(1000);
                    yield return item;
                }
            }
        }

        #endregion

        #region Constructors

        public ReactiveRepository(IRepository<T> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods

        public Task SaveAsync(T entity, Action<T> callback)
        {
            return CreateTask(entity, repository.Save, callback);
        }

        public Task DeleteAsync(T entity, Action<T> callback)
        {
            return CreateTask(entity, repository.Delete, callback);
        }

        public Task CreateAsync(T entity, Action<T> callback)
        {
            return CreateTask(entity, repository.Create, callback);
        }

        private Task CreateTask(T entity, Action<T> action, Action<T> callback)
        {
            return Task.Factory.StartNew(
                () => action(entity))
            .ContinueWith((task) =>
            {
                if (task.IsCompleted)
                {
                    callback(entity);
                }
                else if (task.IsFaulted)
                {
                    callback(entity);
                }
            });
        }

        #endregion
    }
}
