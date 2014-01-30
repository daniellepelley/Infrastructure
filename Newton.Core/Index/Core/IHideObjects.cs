using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Core
{
    /// <summary>
    /// Hides the object methods for ease of use
    /// </summary>
    public interface IHideObjectMembers
    {
        bool Equals(object value);
        int GetHashCode();
        Type GetType();
        string ToString();
    }
}
