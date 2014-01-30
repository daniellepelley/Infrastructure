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
    /// A class that tracks user interaction via controller actions on an MVC application
    /// </summary>
    public class UserTrackerLogAttribute : ActionFilterAttribute, IActionFilter
    {
        #region Properties

        /// <summary>
        /// The contain logger that with log the exception
        /// </summary>
        private ILogger<ActionExecutedContext> logger;

        #endregion

        #region Constructors

        /// <summary>
        /// A class that tracks user interaction via controller actions on an MVC application
        /// </summary>
        public UserTrackerLogAttribute(ILogger<ActionExecutedContext> logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Occurs when an action on a controller is executed
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (logger != null)
                logger.Log(filterContext);

            //Continues to handle the action
            base.OnActionExecuted(filterContext);
        }

        #endregion
    }
}