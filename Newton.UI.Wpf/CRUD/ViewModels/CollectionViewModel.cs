using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Input;

using Newton.UI.Wpf;

namespace Newton.UI.Wpf
{
    public class CollectionViewModel<T>
        : ViewModelBase,
        ICollectionViewModel
    {
        private MainTemplateSelector dataTemplateSelector = new MainTemplateSelector();
        /// <summary>
        /// The data template selector responcible for displaying the entity
        /// </summary>
        public MainTemplateSelector DataTemplateSelector
        {
            get { return dataTemplateSelector; }
            set { dataTemplateSelector = value; }
        }

        private IEnumerable<T> entities;

        public IEnumerable<T> Entities
        {
            get { return entities; }
            set { entities = value; }
        }

        public string EntityName
        {
            get { return EntityToDataTemplate.CleanUpFieldName(entities.GetType().GetGenericArguments()[0].Name); }
        }

        private DataTemplate entityDataTemplate;

        public DataTemplate EntityDataTemplate
        {
            get { return entityDataTemplate; }
            set { entityDataTemplate = value; }
        }

        private Func<T, bool> isDirtyFunc;

        public Func<T, bool> IsDirtyFunc
        {
            get { return isDirtyFunc; }
            set { isDirtyFunc = value; }
        }

        //private Action<object> navigateAction;

        //public Action<object> NavigateAction
        //{
        //    get { return navigateAction; }
        //    set { navigateAction = value; }
        //}

        public CollectionViewModel(IAppNavigationService appNavigationService)
        {
            this.AppNavigationService = appNavigationService;

            CommandBinding openBinding = new CommandBinding(ApplicationCommands.Open);
            openBinding.CanExecute += new CanExecuteRoutedEventHandler(openBinding_CanExecute);
            openBinding.Executed += new ExecutedRoutedEventHandler(openBinding_Executed);
            this.CommandBindings.Add(openBinding);
        }

        void openBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AppNavigationService.Navigate(e.Parameter);
        }

        void openBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null;
        }
    }

    public class CollectionViewModel
        : ViewModelBase,
        ICollectionViewModel
    {
        private MainTemplateSelector dataTemplateSelector = new MainTemplateSelector();
        /// <summary>
        /// The data template selector responcible for displaying the entity
        /// </summary>
        public MainTemplateSelector DataTemplateSelector
        {
            get { return dataTemplateSelector; }
            set { dataTemplateSelector = value; }
        }

        private object entities;

        public object Entities
        {
            get { return entities; }
            set { entities = value; }
        }

        public string EntityName
        {
            get
            {
                return "Test";
    
                return EntityToDataTemplate.CleanUpFieldName(entities.GetType().GetGenericArguments()[0].Name);
            }
        }

        private DataTemplate entityDataTemplate;

        public DataTemplate EntityDataTemplate
        {
            get { return entityDataTemplate; }
            set { entityDataTemplate = value; }
        }

        private Action<object> navigateAction;

        public Action<object> NavigateAction
        {
            get { return navigateAction; }
            set { navigateAction = value; }
        }

        public CollectionViewModel()
        {
            CommandBinding openBinding = new CommandBinding(ApplicationCommands.Open);
            openBinding.CanExecute += new CanExecuteRoutedEventHandler(openBinding_CanExecute);
            openBinding.Executed += new ExecutedRoutedEventHandler(openBinding_Executed);
            this.CommandBindings.Add(openBinding);
        }

        void openBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            navigateAction(e.Parameter);
        }

        void openBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter is System.Collections.IList)
            {
                e.CanExecute = ((System.Collections.IList)e.Parameter).Count > 0;
            }
            else
            {
                e.CanExecute = e.Parameter != null;
            }
        }
    }
}
