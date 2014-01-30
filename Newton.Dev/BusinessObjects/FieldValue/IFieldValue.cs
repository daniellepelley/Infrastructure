using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Newton.Dev.BusinessObjects
{
    /// <summary>
    /// Interface representing an object that holds a value of type T
    /// </summary>
    public interface IFieldValue<T>
    {
        /// <summary>
        /// Value of type T
        /// </summary>
        T Value { get; set; }
    }
}
