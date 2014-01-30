﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    public interface IQuery { }

    public interface IQuery<T>
        : IQuery
        where T : class
    { }
}
