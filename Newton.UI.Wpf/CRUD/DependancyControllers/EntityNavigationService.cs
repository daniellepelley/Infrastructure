using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Newton.UI.Wpf
{
    /// <summary>
    /// Represents an object that contains an app navigation service
    /// </summary>
    public interface IAppNavigationServiceable
    {
        #region Properties

        /// <summary>
        /// The contained app navigation service
        /// </summary>
        IAppNavigationService AppNavigationService { get; set; }

        #endregion
    }

    /// <summary>
    /// Navigates to another viewmodel.
    /// </summary>
    public interface IAppNavigationService
    {
        #region Properties

        IViewResolver ViewResolver { get; set; }

        #endregion

        #region Methods

        void Navigate(object obj);

        #endregion
    }

    /// <summary>
    /// Navigation service matching viewmodels with views
    /// </summary>
    //Navigates around the entities in a way allowed by the set up
    //Responsible for asigning a repository from a factory dependant on the type
    //Assigning a view model dependant on the function
    public class EntityNavigationService : IAppNavigationService
    {
        #region Properties

        private ViewModelFactory viewModelFactory;// = new CRUDViewModelFactory(new Test.Newton.Controls.Wpf.LinqToSQLRepositoryFactory());

        private IViewResolver viewResolver = new ViewResolver();
        /// <summary>
        /// Mappings between viewmodels and views
        /// </summary>
        public IViewResolver ViewResolver
        {
            get { return viewResolver; }
            set { viewResolver = value; }
        }

        private NavigationService navigationService;
        /// <summary>
        /// The contained navigation service
        /// </summary>
        public NavigationService NavigationService
        {
            get { return navigationService; }
            set { navigationService = value; }
        }

        #endregion

        #region Constructors

        public EntityNavigationService(ViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigates to another viewmodel.
        /// </summary>
        public void Navigate(object obj)
        {
            try
            {
                if (obj is IViewModelBase)
                {
                    NavigateToViewModel(obj as IViewModelBase);
                    return;
                }

                Type type = obj.GetType();

                if (obj is System.Collections.IList)
                {
                    type = obj.GetType().GetGenericArguments().FirstOrDefault();
                }

                IViewModelBase viewModel = viewModelFactory.CreateFromType(type);
                viewModel.AppNavigationService = this;
                NavigateToViewModel(viewModel);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Navigates to another viewmodel.
        /// </summary>
        private void NavigateToViewModel(IViewModelBase viewModel)
        {
            try
            {
                if (viewResolver != null)
                {
                    System.Windows.FrameworkElement control = viewResolver.Resolve(viewModel);

                    if (control != null)
                    {
                        control.DataContext = viewModel;
                        navigationService.Navigate(control);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        #endregion
    }







}