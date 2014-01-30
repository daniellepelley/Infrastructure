using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    public interface IDataSecurityProvider
    {
        bool IsSavePermitted<T>();
        bool IsReadPermitted<T>();
        bool IsDeletePermitted<T>();
        bool IsCreatePermitted<T>();
    }
}
