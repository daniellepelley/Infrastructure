using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Newton.Logging.Mvc
{
    /// <summary>
    /// Entry point to configure the logger
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Entry point to configure the logger
        /// </summary>
        public static void Configure(System.Web.HttpApplication application, string configFilePath)
        {
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(configFilePath));
        }
    }
}
