using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Data
{
    /// <summary>
    /// Interface representing a logger
    /// </summary>
    public interface ILogger<T> where T : class
    {
        /// <summary>
        /// Creates a log for the passed entity
        /// </summary>
        void Log(T entity);
    }
}
