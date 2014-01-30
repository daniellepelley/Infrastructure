using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.Mvc;

using Newton.Data;

namespace Newton.Logging.Mvc
{
    /// <summary>
    /// Logs controllers and actions
    /// </summary>
    public class ActionExecutedLogger : StructuredLogger<ActionExecutedContext>
    {
        #region Methods

        /// <summary>
        /// Defines the structue of the log entries
        /// </summary>
        public override IEnumerable<LogStructureItem<ActionExecutedContext>> CreateLogStructure()
        {
            List<LogStructureItem<ActionExecutedContext>> list = new List<LogStructureItem<ActionExecutedContext>>();
            list.Add(new LogStructureItem<ActionExecutedContext>("UserName", f => f.HttpContext.User.Identity.Name));
            list.Add(new LogStructureItem<ActionExecutedContext>("IP", f => f.HttpContext.Request.UserHostAddress));
            list.Add(new LogStructureItem<ActionExecutedContext>("Browser", f => f.HttpContext.Request.Browser.Browser));
            list.Add(new LogStructureItem<ActionExecutedContext>("BrowserVersion", f => f.HttpContext.Request.Browser.Version.ToString()));
            list.Add(new LogStructureItem<ActionExecutedContext>("JScriptVersion", f => f.HttpContext.Request.Browser.JScriptVersion.ToString()));
            list.Add(new LogStructureItem<ActionExecutedContext>("LogonUserIdentity", f => f.HttpContext.Request.LogonUserIdentity.Name));
            list.Add(new LogStructureItem<ActionExecutedContext>("Controller", f => f.ActionDescriptor.ControllerDescriptor.ControllerName));
            list.Add(new LogStructureItem<ActionExecutedContext>("Action", f => f.ActionDescriptor.ActionName));
            return list;
        }

        /// <summary>
        /// Logs controllers and actions
        /// </summary>
        public override void Log(ActionExecutedContext logObject)
        {
            try
            {
                if (LogStructure != null &&
                    logObject != null)
                {
                    //log4net.ILog logger = log4net.LogManager.GetLogger("Action");

                    //lock (logger)
                    //{
                        //logger.Info(LogStructure.BuildMessage(logObject));
                    //}
                }
            }
            catch(Exception ex)
            {
            }
        }

        #endregion
    }
}