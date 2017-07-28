using System;
using System.Web.UI;

namespace PremiosInstitucionales
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Resources/js/jquery.min.js",
                DebugPath = "~/Resources/js/jquery.min.js",
                CdnPath = "~/Resources/js/jquery.min.js",
                CdnDebugPath = "~/Resources/js/jquery.min.js"
            });
        }
    }
}