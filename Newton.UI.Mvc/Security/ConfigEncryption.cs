using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.UI.Mvc.Security
{
    public class ConfigEncrypter
    {
        //public ActionResult Encrypt()
        //{
        //    ProtectSection("WhiteboardMachineLogins", "RSAProtectedConfigurationProvider");
        //    ProtectSection("connectionStrings", "RSAProtectedConfigurationProvider");
        //    ProtectSection("appSettings", "RSAProtectedConfigurationProvider");
        //    return View();
        //}

        public void ProtectSection(string sectionName, string provider)
        {
            throw (new NotImplementedException());
            //System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);

            //var section = config.GetSection(sectionName);

            //if (section != null && !section.SectionInformation.IsProtected)
            //{
            //    section.SectionInformation.ProtectSection(provider);
            //    config.Save();
            //}
        }
    }
}
