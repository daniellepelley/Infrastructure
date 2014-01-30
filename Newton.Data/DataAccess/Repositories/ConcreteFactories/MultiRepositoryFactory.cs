using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A repository factories that can contain inner repository factories
    /// </summary>
    public class MultiRepositoryFactory : IRepositoryFactory
    {
        private List<IRepositoryFactory> repositoryFactories = new List<IRepositoryFactory>();
        /// <summary>
        /// Collection of repository factories
        /// </summary>
        public List<IRepositoryFactory> RepositoryFactories
        {
            get { return repositoryFactories; }
        }

        /// <summary>
        /// Creates an IRepository for type T
        /// </summary>
        public IRepository<T> Create<T>() where T : class
        {
            foreach (IRepositoryFactory repositoryFactory in repositoryFactories)
            {
                IRepository<T> repository = repositoryFactory.Create<T>();

                if (repository != null)
                    return repository;
            }
            return null;
        }
    }
}
