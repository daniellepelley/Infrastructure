using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Newton.Data.DataAccess.Repositories.Reactive
{
    public class ReactiveRepository<T> : IAsyncRepository<T>
    {
        private IRepository<T> repository;
        public IDataContext DataContext
        {
            get { return repository.DataContext; }
        }

        public IObservable<T> Items
        {
            get { return repository.Items.ToObservable(); }
        }

        public async Task SaveAsync(T entity)
        {
            await Task.Factory.StartNew(
                () => repository.Save(entity)
                );
        }

        public async void DeleteAsync(T entity)
        {
            await Task.Factory.StartNew(
                () => repository.Delete(entity)
                );
        }

        public async void CreateAsync(T entity)
        {
            await Task.Factory.StartNew(
                () => repository.Create(entity)
                );
        }
    }
}
