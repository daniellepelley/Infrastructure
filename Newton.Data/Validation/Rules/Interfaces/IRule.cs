using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// Interface representing an item that performs a validation check.
    /// </summary>
    public interface IRule
    {
        string Text { get; }
    }
}
