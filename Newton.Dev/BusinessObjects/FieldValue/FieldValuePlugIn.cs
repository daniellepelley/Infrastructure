using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Newton.Dev.BusinessObjects
{
    /// <summary>
    /// A class representing a value in a field.
    /// </summary>
    public class FieldValuePlugIn<T>
        : NotifyPropertyChanged, 
        IFieldValue<T>,
        IIsDirty
    {
        #region Parameter

        private T originalValue;

        /// <summary>
        /// Whether or not the value has changed since the last mark as clean
        /// </summary>
        public bool IsDirty
        {
            get { return !originalValue.Equals(Value); }
        }

        private Action<T> setValue;
        /// <summary>
        /// The action that sets the value
        /// </summary>
        public Action<T> SetValue
        {
            get { return setValue; }
            set { setValue = value; }
        }

        private Func<T> getValue;
        /// <summary>
        /// The action that gets the value
        /// </summary>
        public Func<T> GetValue
        {
            get { return getValue; }
            set { getValue = value; }
        }

        /// <summary>
        /// The value for this field value;
        /// </summary>
        public T Value
        {
            set
            {
                if (setValue == null)
                    throw (new Exception("SetValue not set"));
                setValue(value);
            }
            get
            {
                if (getValue == null)
                    throw (new Exception("GetValue not set"));
                return getValue();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// A class representing a value in a field.
        /// </summary>
        public FieldValuePlugIn(Action<T> setValue, Func<T> getValue)
        {
            this.setValue = setValue;
            this.getValue = getValue;
        }

        #endregion

        #region Methods

        public void MarkAsClean()
        {
            originalValue = Value;
        }

        #endregion
    }

    public interface IIsDirty
    {
        bool IsDirty { get; }
    }

    /// <summary>
    /// A class representing a value in a field.
    /// </summary>
    public class NotifyPropertyChanged
        : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
