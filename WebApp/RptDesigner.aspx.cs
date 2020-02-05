using Radyn.WebApp.Areas.Common.Tools;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using System;


namespace Radyn.WebApp
{
    public partial class RptDesigner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["DataMethod"]))
                {
                    var info = new ReportProvider().GetType().GetMethod(System.Web.HttpContext.Current.Request.QueryString["DataMethod"]);
                    if (info != null)
                    {
                        var invoke = info.Invoke(info, string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["DataMethodParamets"]) ? null : new[] { System.Web.HttpContext.Current.Request.QueryString["DataMethodParamets"] });
                        if (invoke != null)
                        {
                            //if (SessionParameters.Culture != null && SessionParameters.Culture == "fa-IR")
                            //    StiConfig.LoadLocalization(Server.MapPath("~/XmlResources/fa.xml"));
                            Stimulsoft.Base.Localization.StiLocalization.Load(Server.MapPath("~/XmlResources/en.xml"));
                            StiWebDesigner1.Report = new StiReport();
                            StiWebDesigner1.Report.Load((byte[])invoke);
                            StiWebDesigner1.Report.Design();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DivError.Visible = true;
                var txt = ex.Message;
                if (ex.InnerException != null)
                    txt += ex.InnerException;
                lblErrorMessage.Text = txt;

            }



        }
    



        protected void StiWebDesigner1_OnSaveReport(object sender, StiWebDesigner.StiSaveReportEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["UpdateMethod"]))
                {
                    var info = new ReportProvider().GetType().GetMethod(System.Web.HttpContext.Current.Request.QueryString["UpdateMethod"]);
                    if (info != null)
                    {


                        info.Invoke(info, string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["UpdateParametrs"]) ? null : new object[] { System.Web.HttpContext.Current.Request.QueryString["UpdateParametrs"], e.Report.SaveToByteArray() });
                    }
                }
            }
            catch (Exception ex)
            {
                DivError.Visible = true;
                var txt = ex.Message;
                if (ex.InnerException != null)
                    txt += ex.InnerException;
                lblErrorMessage.Text = txt;

            }
        }
    }
}