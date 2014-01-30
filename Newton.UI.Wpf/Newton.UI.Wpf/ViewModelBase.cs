using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Newton.Core;

namespace Newton.UI.Wpf
{
    /// <summary>
    /// Class representing the basic requirements for a view model
    /// </summary>
    public class ViewModelBase : 
        NotifyPropertyChanged,
        IViewModelBase
    {
        #region Parameters

        private IAppNavigationService appNavigationService;
        /// <summary>
        /// The contained navigation service
        /// </summary>
        public IAppNavigationService AppNavigationService
        {
            get { return appNavigationService; }
            set { appNavigationService = value; }
        }

        private List<CommandBinding> commandBindings = new List<CommandBinding>();

        /// <summary>
        /// List of command bindings for the view model
        /// </summary>
        public List<CommandBinding> CommandBindings
        {
            get { return commandBindings; }
        }

        #endregion
    }
}
