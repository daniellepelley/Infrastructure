using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Input;

using Newton.UI.Wpf;

namespace Newton.UI.Wpf
{
    public class EditViewModel<T>
        : ViewModelBase,
        IEditViewModel
    {
        #region Properties

        private T entity;
        /// <summary>
        /// The entity contained within the viewmodel
        /// </summary>
        public T Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        public string EntityName
        {
            get
            {
                if (entity == null)
                    return string.Empty;

                return EntityToDataTemplate.CleanUpFieldName(entity.GetType().Name);
            }
        }

        private MainTemplateSelector dataTemplateSelector = new MainTemplateSelector();
        /// <summary>
        /// The data template selector responcible for displaying the entity
        /// </summary>
        public MainTemplateSelector DataTemplateSelector
        {
            get { return dataTemplateSelector; }
            set { dataTemplateSelector = value; }
        }

        #endregion

        //private DataTemplate entityDataTemplate;

        //public DataTemplate EntityDataTemplate
        //{
        //    get { return entityDataTemplate; }
        //    set { entityDataTemplate = value; }
        //}

        private Func<T, bool> isDirtyFunc;

        public Func<T, bool> IsDirtyFunc
        {
            get { return isDirtyFunc; }
            set { isDirtyFunc = value; }
        }

        //private Action saveAction;

        //public Action SaveAction
        //{
        //    get { return saveAction; }
        //    set { saveAction = value; }
        //}

        //private Action<object> navigateAction;

        //public Action<object> NavigateAction
        //{
        //    get { return navigateAction; }
        //    set { navigateAction = value; }
        //}

        #region Constructors

        public EditViewModel(IAppNavigationService appNavigationService)
        {
            this.AppNavigationService = appNavigationService;

            CommandBinding saveBinding = new CommandBinding(ApplicationCommands.Save);
            saveBinding.CanExecute += new CanExecuteRoutedEventHandler(saveBinding_CanExecute);
            saveBinding.Executed += new ExecutedRoutedEventHandler(saveBinding_Executed);
            this.CommandBindings.Add(saveBinding);

            CommandBinding openBinding = new CommandBinding(ApplicationCommands.Open);
            openBinding.CanExecute += new CanExecuteRoutedEventHandler(openBinding_CanExecute);
            openBinding.Executed += new ExecutedRoutedEventHandler(openBinding_Executed);
            this.CommandBindings.Add(openBinding);
        }

        #endregion

        #region Command Methods

        void openBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AppNavigationService.Navigate(e.Parameter);
        }

        void openBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null;
        }

        void saveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //saveAction();
        }

        void saveBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirtyFunc(entity);
        }

        #endregion
    }

    //public class EditViewModel : ViewModelBase
    //{
    //    private object entity;

    //    public object Entity
    //    {
    //        get { return entity; }
    //        set { entity = value; }
    //    }

    //    public string EntityName
    //    {
    //        get { return EntityToDataTemplate.CleanUpFieldName(entity.GetType().Name); }
    //    }

    //    private MainTemplateSelector dataTemplateSelector = new MainTemplateSelector();

    //    public MainTemplateSelector DataTemplateSelector
    //    {
    //        get { return dataTemplateSelector; }
    //        set { dataTemplateSelector = value; }
    //    }

    //    private Func<object, bool> isDirtyFunc;

    //    public Func<object, bool> IsDirtyFunc
    //    {
    //        get { return isDirtyFunc; }
    //        set { isDirtyFunc = value; }
    //    }

    //    private Action saveAction;

    //    public Action SaveAction
    //    {
    //        get { return saveAction; }
    //        set { saveAction = value; }
    //    }

    //    private Action<object> navigateAction;

    //    public Action<object> NavigateAction
    //    {
    //        get { return navigateAction; }
    //        set { navigateAction = value; }
    //    }

    //    public EditViewModel()
    //    {
    //        CommandBinding saveBinding = new CommandBinding(ApplicationCommands.Save);
    //        saveBinding.CanExecute += new CanExecuteRoutedEventHandler(saveBinding_CanExecute);
    //        saveBinding.Executed += new ExecutedRoutedEventHandler(saveBinding_Executed);
    //        this.CommandBindings.Add(saveBinding);

    //        CommandBinding openBinding = new CommandBinding(ApplicationCommands.Open);
    //        openBinding.CanExecute += new CanExecuteRoutedEventHandler(openBinding_CanExecute);
    //        openBinding.Executed += new ExecutedRoutedEventHandler(openBinding_Executed);
    //        this.CommandBindings.Add(openBinding);
    //    }

    //    void openBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    //    {
    //        navigateAction(e.Parameter);
    //    }

    //    void openBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    //    {
    //        if (e.Parameter is System.Collections.IList)
    //        {
    //            e.CanExecute = ((System.Collections.IList)e.Parameter).Count > 0;
    //        }
    //        else
    //        {
    //            e.CanExecute = e.Parameter != null;
    //        }
    //    }

    //    void saveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    //    {
    //        saveAction();
    //    }

    //    void saveBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    //    {
    //        e.CanExecute = isDirtyFunc(entity);
    //    }
    //}
}
