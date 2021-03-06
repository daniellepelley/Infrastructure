﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    public interface IEnforcable
    { }

    public interface IEnforcable<T> : IEnforcable
    {
        T Enforce(T value);
    }
}
