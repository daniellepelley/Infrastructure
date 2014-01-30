using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A factory responcible for creating an instance of a repository
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates an IRepository for type T
        /// </summary>
        IRepository<T> Create<T>() where T : class;
    }
}
