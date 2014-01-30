using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Data;
using Newton.UI.Wpf;

namespace Newton.UI.Wpf
{
    /// <summary>
    /// A factory to create a generic typed View Model
    /// </summary>
    public class CRUDViewModelFactory : ViewModelFactory
    {
        #region Properties

        private CRUDServiceFactory serviceFactory;
        /// <summary>
        /// Factory responcible for create the services
        /// </summary>
        public CRUDServiceFactory ServiceFactory
        {
            get { return serviceFactory; }
        }

        private IRepositoryFactory repositoryFactory;

        public IRepositoryFactory RepositoryFactory
        {
            get { return repositoryFactory; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A factory to create a generic typed View Model
        /// </summary>
        public CRUDViewModelFactory(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
            serviceFactory = new CRUDServiceFactory();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a generic typed View Model
        /// </summary>
        public override IViewModelBase Create<T>()
        {
            CRUDService<T> service = serviceFactory.Create<T>();
            service.Repository = repositoryFactory.Create<T>();

            var viewModel = new EditViewModel<T>(new EntityNavigationService(this));

            viewModel.Entity = service.Items.FirstOrDefault();

            viewModel.IsDirtyFunc = e =>
            {
                return service.AreChanges();
            };

            //viewModel.SaveAction = () => service.
            return viewModel;
        }

        /// <summary>
        /// Creates a generic typed View Model
        /// </summary>
        public virtual IViewModelBase CreateFromType(Type type)
        {
            return (IViewModelBase)Newton.Reflection.Reflection.Invoke(this, "Create", type, null);
        }

        #endregion
    }

    public class TestLogger<T> : ILogger<T> where T : class
    {
        public void Log(T item)
        {
            
        }
    }

    //public class TestSecurity : IDataSecurityProvider
    //{
    //    public bool IsPermitted<T>()
    //    {
    //        if (typeof(T) == typeof(PasPlus.Data.LTS.Hospital))
    //            return false;

    //        return true;
    //    }
    //}

    public class TestCRUDViewModelFactory : CRUDViewModelFactory
    {
        #region Constructors

        /// <summary>
        /// A factory to create a generic typed View Model
        /// </summary>
        public TestCRUDViewModelFactory(IRepositoryFactory repositoryFactory)
            :base(repositoryFactory)
        { }

        #endregion

        public override IViewModelBase Create<T>()
        {
            CRUDService<T> service = ServiceFactory.Create<T>();
            service.Repository = RepositoryFactory.Create<T>();
            //service.Repository = new LoggableRepository<T>(service.Repository, new TestLogger<T>());
            //service.Repository = new SecureRepository<T>(service.Repository, new TestSecurity());



            //var viewModel = new EditViewModel<T>(new EntityNavigationService(this));

            //viewModel.Entity = service.Items.FirstOrDefault();

            var viewModel = new CollectionViewModel<T>(new EntityNavigationService(this));

            viewModel.Entities = service.Items;

            viewModel.IsDirtyFunc = e =>
            {
                return service.AreChanges();
            };

            //viewModel.SaveAction = () => service.
            return viewModel;
        }
    }

}
