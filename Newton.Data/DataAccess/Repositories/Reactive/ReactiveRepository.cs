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

        public async Task SaveAsync(T entity)
        {
            await Task.Factory.StartNew(() => repository.Save(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Factory.StartNew(() => repository.Delete(entity));
        }

        public async Task CreateAsync(T entity)
        {
            await Task.Factory.StartNew(() => repository.Create(entity));
        }
    }
}
