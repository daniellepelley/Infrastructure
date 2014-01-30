using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newton.Data
{
    /// <summary>
    /// Class representing a structure key pair logger
    /// </summary>
    public abstract class StructuredLogger<T> : ILogger<T>
        where T : class
    {
        #region Properties

        private LogStructure<T> logStructure = new LogStructure<T>();
        /// <summary>
        /// The structure of the data to be added to the log
        /// </summary>
        public LogStructure<T> LogStructure
        {
            get { return logStructure; }
            set { logStructure = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Class representing a structure key pair logger
        /// </summary>
        public StructuredLogger()
        {
            LogStructure.Items.AddRange(CreateLogStructure());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Defines the structue of the log entries
        /// </summary>
        public virtual IEnumerable<LogStructureItem<T>> CreateLogStructure()
        {
            return new List<LogStructureItem<T>>();
        }

        /// <summary>
        /// Logs an exception using an ExceptionContext on an MVC application
        /// </summary>
        public abstract void Log(T logObject);

        #endregion
    }
}