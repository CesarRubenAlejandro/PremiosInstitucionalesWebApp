using System;
using System.Web;
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

        protected void Application_Error(object sender, EventArgs e)
        {
            // An error has occured on a .Net page.
            var serverError = Server.GetLastError() as HttpException;

            if (null != serverError)
            {
                int errorCode = serverError.GetHttpCode();

                if (404 == errorCode)
                {
                    Server.ClearError();
                    Server.Transfer("/WebForms/Error404.aspx");
                }
            }
        }
    }
}