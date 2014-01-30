using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.Mvc;

using Newton.Data;

namespace Newton.Logging.Mvc
{
    /// <summary>
    /// Logs an exception using an ExceptionContext on an MVC application
    /// </summary>
    public class ExceptionLogger : StructuredLogger<ExceptionContext>
    {
        #region Properties

        private ILoggerFactory loggerFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Logs an exception using an ExceptionContext on an MVC application
        /// </summary>
        public ExceptionLogger(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Defines the structue of the log entries
        /// </summary>
        public override IEnumerable<LogStructureItem<ExceptionContext>> CreateLogStructure()
        {
            List<LogStructureItem<ExceptionContext>> list = new List<LogStructureItem<ExceptionContext>>();
            list.Add(new LogStructureItem<ExceptionContext>("UserName", f => f.HttpContext.User.Identity.Name));
            list.Add(new LogStructureItem<ExceptionContext>("IP", f => f.HttpContext.Request.UserHostAddress));
            list.Add(new LogStructureItem<ExceptionContext>("Browser", f => f.HttpContext.Request.Browser.Browser));
            list.Add(new LogStructureItem<ExceptionContext>("BrowserVersion", f => f.HttpContext.Request.Browser.Version.ToString()));
            list.Add(new LogStructureItem<ExceptionContext>("JScriptVersion", f => f.HttpContext.Request.Browser.JScriptVersion.ToString()));
            list.Add(new LogStructureItem<ExceptionContext>("LogonUserIdentity", f => f.HttpContext.Request.LogonUserIdentity.Name));
            return list;
        }

        /// <summary>
        /// Logs an exception using an ExceptionContext on an MVC application
        /// </summary>
        public override void Log(ExceptionContext logObject)
        {
            try
            {
                if (LogStructure != null &&
                    logObject != null)
                {
                    var logger = loggerFactory.Create("ExceptionLogger");

                    //log4net.ILog logger = log4net.LogManager.GetLogger("ExceptionLogger");

                    lock (logger)
                    {
                        //logger.Error(LogStructure.BuildMessage(logObject), logObject.Exception);
                    }
                }
            }
            catch { }
        }

        #endregion
    }
}