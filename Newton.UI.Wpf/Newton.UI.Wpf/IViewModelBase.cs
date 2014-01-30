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

namespace Newton.UI.Wpf
{
    /// <summary>
    /// Interface representing the basic requirements for a view model
    /// </summary>
    public interface IViewModelBase
        : INotifyPropertyChanged,
        IAppNavigationServiceable
    {
        #region Events

        /// <summary>
        /// Occurs when a property has changed
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        void OnPropertyChanged(string propertyName);

        #endregion

        #region Parameters

        IAppNavigationService AppNavigationService { get; set; }

        /// <summary>
        /// List of command bindings for the view model
        /// </summary>
        List<CommandBinding> CommandBindings { get; }

        #endregion
    }
}