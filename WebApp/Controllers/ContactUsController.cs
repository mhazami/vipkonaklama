using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.Compilation;
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
    public class ContactUsController : LocalizedController
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
                if (string.IsNullOrEmpty(obj.Email.Trim()))
                    list.Add("لطفا ایمیل را وارد نمایید");
                var messageBody = list.Aggregate("", (current, str) => current + Environment.NewLine + (str));
                if (!string.IsNullOrEmpty(messageBody))
                    throw new KnownException(messageBody);
                obj.Title = string.Format("تماس با {0}", obj.Name);
                obj.Description += "\n نام:" + obj.Name +
                                   (string.IsNullOrEmpty(obj.Title) ? "" : "\n موضوع:" + obj.Title) +
                                   (string.IsNullOrEmpty(obj.Email) ? "" : "\n ایمیل  :" + obj.Email);
                var bodys = this.GetDetailForm(collection);
              obj.Description += "\n" + bodys;
                var requestDemoModel = new SaleSupportService.DealModel();
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
                    catch (Exception ex)
                    {


                    }
                    var contractUsEmail = Radyn.Message.Tools.MessageServiceBody.GetEmailBody("با سلام واحترام یک درخواست تماس با ما به شرح زیر برای شما ارسال گردید", obj.Description);
                    if (MessageComponenet.Instance.MailFacade.SendMail(SessionParameters.CurrentWebSite.Configuration.MailHost, SessionParameters.CurrentWebSite.Configuration.MailPassword, SessionParameters.CurrentWebSite.Configuration.MailUserName, SessionParameters.CurrentWebSite.Configuration.MailFrom, SessionParameters.CurrentWebSite.Configuration.MailPort,
                        SessionParameters.CurrentWebSite.Configuration.MailFrom, SessionParameters.CurrentWebSite.Configuration.EnableSSL, "info@" + Request.Url.Authority, "تماس با ما", contractUsEmail))
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
