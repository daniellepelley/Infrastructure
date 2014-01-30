using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// Interface representing an item that performs a validation check.
    /// </summary>
    public interface IRule<T>
        : IRule
    {
        string Check(T value);
        bool CheckValid(T value);
    }
}