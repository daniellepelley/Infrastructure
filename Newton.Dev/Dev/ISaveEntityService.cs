using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    public interface ISaveEntityService<T>
    {
        T Entity { get; }
        void Save();
        bool IsDirty { get; }
    }
}
