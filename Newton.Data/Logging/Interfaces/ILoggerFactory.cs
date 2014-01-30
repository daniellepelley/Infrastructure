using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// A factory which creates a logger
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates a logger from a type
        /// </summary>
        ILogger<T> Create<T>() where T : class;

        /// <summary>
        /// Creates a generic logger
        /// </summary>
        ILogger<object> Create(string loggerName);
    }
}
