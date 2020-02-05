using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using Radyn.Framework;
using Radyn.Message;
using Radyn.Security.Tools;
using Radyn.Web.Mvc.UI.Captcha;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.FormGenerator.Tools;

namespace Radyn.WebApp.Controllers
{
    public class RequestDemoController : LocalizedController
    {

        [WebDesignHost]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var obj = new ContactUSModel();
                var requestDemoModel = new SaleSupportService.DealModel();
                this.RadynTryUpdateModel(obj, collection);
                var list = new List<string>();
                if (!Request.Url.Authority.Contains("localhost"))
                {
                    var service = new CaptchaService();
                    if (!service.IsValidCaptcha(collection["captch"]))
                    {
                        list.Add(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                    }
                }
                if (string.IsNullOrEmpty(obj.Name.Trim()))
                    list.Add("لطفا نام را وارد نمایید");
                if (string.IsNullOrEmpty(obj.Tel.Trim()))
                    list.Add("لطفا شماره تماس را وارد نمایید");
                var messageBody = list.Aggregate("", (current, str) => current + Environment.NewLine + (str));
                if (!string.IsNullOrEmpty(messageBody))
                    throw new KnownException(messageBody);
                obj.Title = string.Format("درخواست سرویس {0}", obj.Name);
                obj.Description += "\n" +
                                   " از طرف:" + obj.Name +
                                   (string.IsNullOrEmpty(obj.CompanyName)
                                       ? ""
                                       : "\n" + " نام سازمان:" + obj.CompanyName) +
                                   (string.IsNullOrEmpty(obj.Email) ? "" : "\n" + " ایمیل  :" + obj.Email) +
                                   (string.IsNullOrEmpty(obj.Tel) ? "" : "\n" + " شماه تماس  :" + obj.Tel) +
                                   (string.IsNullOrEmpty(obj.TelInternal) ? "" : "\n" + " داخلی  :" + obj.TelInternal);
                var bodys = this.GetDetailForm(collection);
                obj.Description += "\n" + bodys;
                if (SessionParameters.CurrentWebSite.Configuration != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(SessionParameters.CurrentWebSite.Configuration.CRMServiceUrl))
                        {
                            Radyn.Utility.Utils.FillObject(requestDemoModel, obj);
                            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                            EndpointAddress address = new EndpointAddress(SessionParameters.CurrentWebSite.Configuration.CRMServiceUrl);
                            new SaleSupportService.SaleSupportClient(binding, address).RequestDeal(SessionParameters.CurrentWebSite.Configuration.CRMServiceUserName, Radyn.Utility.StringUtils.Decrypt(SessionParameters.CurrentWebSite.Configuration.CRMServicePassword), requestDemoModel);
                        }
                        
                    }
                    catch(Exception ex)
                    {


                    }
                    var requestDemoEmail = Radyn.Message.Tools.MessageServiceBody.GetEmailBody("با سلام واحترام یک درخواست دمو به شرح زیر برای شما ارسال گردید", obj.Description);
                    if (MessageComponenet.Instance.MailFacade.SendMail(SessionParameters.CurrentWebSite.Configuration.MailHost, SessionParameters.CurrentWebSite.Configuration.MailPassword, SessionParameters.CurrentWebSite.Configuration.MailUserName, SessionParameters.CurrentWebSite.Configuration.MailFrom, SessionParameters.CurrentWebSite.Configuration.MailPort, SessionParameters.CurrentWebSite.Configuration.MailFrom, SessionParameters.CurrentWebSite.Configuration.EnableSSL, "info@" + Request.Url.Authority
                        , "درخواست دمو", requestDemoEmail))
                        ShowMessage(Resources.CommonComponent.SentAdminMessage);
                }
                

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
            }
            return View();
        }

    }
}
