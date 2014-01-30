using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Defines an object that can log changes to itself
    /// </summary>
    public interface ILoggable<T> where T : class
    {
        ILogger<T> Logger { get; }
    }
}
