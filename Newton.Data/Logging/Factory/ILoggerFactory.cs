using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Objects;

namespace Newton.Data
{
    /// <summary>
    /// A factory which produces Entity Framework repositories
    /// </summary>
    public abstract class LoggerFactory : ILoggerFactory
    {
        public ILogger<T> Create<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public ILogger<object> Create(string loggerName)
        {
            throw new NotImplementedException();
        }
    }
}