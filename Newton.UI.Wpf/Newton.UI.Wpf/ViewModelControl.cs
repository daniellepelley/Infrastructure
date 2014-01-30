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
    ///// <summary>
    ///// A control designed to support a viewmodel
    ///// </summary>
    //public class ViewModelControlTest : ContentControl
    //{
    //    public readonly static DependencyProperty ViewModelProperty;
    //    /// <summary>
    //    /// The contained ViewModel
    //    /// </summary>
    //    public IViewModelBase ViewModel
    //    {
    //        set { SetValue(ViewModelProperty, value); }
    //        get { return (IViewModelBase)GetValue(ViewModelProperty); }
    //    }

    //    public ViewModelControlTest()
    //        : base()
    //    { }

    //    static ViewModelControlTest()
    //    {
    //        //ViewModelControl.DataContextProperty.DefaultMetadata.

    //        PropertyMetadata metadata = new PropertyMetadata(CallBack);
    //        ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(IViewModelBase), typeof(ViewModelControl), metadata);
    //    }

    //    public static void CallBack(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    //    {
    //        ViewModelControlTest myClass = (ViewModelControlTest)dependencyObject;

    //        if (e.NewValue == null)
    //            myClass.ViewModel = null;
    //        else
    //            myClass.ViewModel = (IViewModelBase)e.NewValue;

    //        myClass.CommandBindings.Clear();

    //        if (myClass.ViewModel != null)
    //            myClass.CommandBindings.AddRange(myClass.ViewModel.CommandBindings);
    //    }
    //}

    /// <summary>
    /// A control designed to support a viewmodel
    /// </summary>
    public class ViewModelControl : ContentControl, IDisposable
    {
        #region Constructors

        /// <summary>
        /// A control designed to support a viewmodel
        /// </summary>
        public ViewModelControl()
            : base()
        { }

        /// <summary>
        /// A control designed to support a viewmodel
        /// </summary>
        static ViewModelControl()
        {
            FrameworkElement.DataContextProperty.AddOwner(typeof(ViewModelControl), new FrameworkPropertyMetadata(CallBack)); 
        }

        #endregion

        #region Static Methods

        public static void CallBack(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ViewModelControl.DataContextProperty)
            {
                ViewModelControl myClass = (ViewModelControl)dependencyObject;

                myClass.CommandBindings.Clear();

                if (e.NewValue != null &&
                    e.NewValue is IViewModelBase)
                {
                    myClass.CommandBindings.AddRange(((IViewModelBase)e.NewValue).CommandBindings);
                }
            }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            this.CommandBindings.Clear();
        }

        #endregion
    }
}