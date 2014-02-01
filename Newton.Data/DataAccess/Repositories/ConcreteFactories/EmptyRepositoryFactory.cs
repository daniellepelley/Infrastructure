using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newton.Data
{
    public class EmptyRepositoryFactory : IRepositoryFactory
    {
        #region Methods

        public IRepository<T> Create<T>() where T : class
        {
            return new EmptyRepository<T>();
        }
        
        #endregion
    }
}

