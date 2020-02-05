using System;
using Radyn.WebApp.AppCode.Security;
using Stimulsoft.Report;

namespace Radyn.WebApp
{
    public partial class RptViewer : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionParameters.Culture != null && SessionParameters.Culture == "fa-IR")
                StiConfig.LoadLocalization(Server.MapPath("~/XmlResources/fa.xml"));
            StiWebViewer1.Report = SessionParameters.Report;
            

        }

      
    }
}