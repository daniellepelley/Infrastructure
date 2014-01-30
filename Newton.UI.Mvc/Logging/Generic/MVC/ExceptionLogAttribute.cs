using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Management;

using Newton.Data;

namespace Newton.Logging.Mvc
{
    /// <summary>
    /// A class used to intersect errors on a ASP MVC application and send them to a logger
    /// </summary>
    public class ExceptionLogAttribute : HandleErrorAttribute
    {
        #region Properties

        /// <summary>
        /// The contain logger that with log the exception
        /// </summary>
        private ILogger<ExceptionContext> logger;

        #endregion

        #region Constructors

        /// <summary>
        /// A class used to intersect errors on a ASP MVC application and send them to a logger
        /// </summary>
        public ExceptionLogAttribute(ILogger<ExceptionContext> logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Occurs when an error is throw
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            if (logger != null)
            {
                logger.Log(context);
            }

            //Continue handling the error
            base.OnException(context);
            new WebRequestErrorEventMvc("An unhandled exception has occurred.", this, 103005, context.Exception).Raise();
        }

        #endregion
    }

    /// <summary>
    /// A WebRequestErrorEvent with public constructors
    /// </summary>
    public class WebRequestErrorEventMvc : WebRequestErrorEvent
    {
        public WebRequestErrorEventMvc(string message, object eventSource, int eventCode, Exception exception) : base(message, eventSource, eventCode, exception) { }
        public WebRequestErrorEventMvc(string message, object eventSource, int eventCode, int eventDetailCode, Exception exception) : base(message, eventSource, eventCode, eventDetailCode, exception) { }
    }
}