using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Newton.Dev.BusinessObjects
{
    /// <summary>
    /// An object that hold value of type T
    /// </summary>
    public class FieldValue<T>
        : IFieldValue<T>
    {
        #region Properties

        private T value;
        /// <summary>
        /// The value of type T
        /// </summary>
        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #endregion
    }
}