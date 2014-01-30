using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Newton.Core
{
    /// <summary>
    /// Class containing a NotifyPropertyChanged event
    /// </summary>
    public class NotifyPropertyChanged
        : INotifyPropertyChanged,
        IDisposable
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Parameters

        protected virtual IEnumerable<IDisposable> DisposableChildren
        {
            get { return new IDisposable[0]; }
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            PropertyChanged = null;

            foreach (IDisposable disposable in DisposableChildren)
                disposable.Dispose();
        }

        #endregion
    }
}
