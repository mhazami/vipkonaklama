using System;
using System.Web.Mvc;
using Radyn.Message;
using Radyn.Message.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Filter;


namespace Radyn.WebApp.Areas.Message.Controllers
{
    public class MessageController : WebDesignBaseController
    {
        [SourceCodeFile("ImageBrowser Controller", "~/Controllers/ImageBrowserController.cs")]

        public ActionResult GetMessagePanel()
        {
            return PartialView("PartialViewMessage", new ModelView.MessageModel());
        }

        public ActionResult MessageIndex(string value, string callBackUrl)
        {

            ViewBag.Delivers = new SelectList(Utility.EnumUtils.ConvertEnumToIEnumerable<Enums.DeliveredType>(), "key", "Value");
            ViewBag.str = value;
            ViewBag.CallBackUrl = callBackUrl;
            return View();
        }
        public ActionResult ResendSMS(Guid Id)
        {
            try
            {
                if (MessageComponenet.Instance.SMSFacade.ResendSMS(Id))
                {
                    ShowMessage(Resources.Message.MessageSentSucced, Resources.Common.MessaageTitle);
                    return Content("true");
                }
                ShowMessage(Resources.Message.ErrorInSendSMS, Resources.Common.MessaageTitle);
                return Content("true");
            }
            catch (Exception exception)
            {

                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        public ActionResult GetSMSIndex(FormCollection collection)
        {
            try
            {
                if (string.IsNullOrEmpty(collection["value"])) return null;
                var str = StringUtils.Decrypt(collection["value"]);
                var strings = str.Split(',');
                var fromdate = collection["DateFrom"];
                var accountNumber = strings[0];
                var username = strings[1];
                var password = strings[2];
                var todate = collection["DateTo"];
                var state = collection["DeliveredState"];
                var status = (string.IsNullOrEmpty(state) ? (byte?)null : (byte)state.ToEnum<Enums.DeliveredType>());
                var list = MessageComponenet.Instance.SMSFacade.Search(accountNumber.ToInt(), username, password, fromdate, todate, collection["txtSearch"], status);
                return PartialView("partialViewMessages", list);
            }
            catch (Exception exception)
            {

                ShowExceptionMessage(exception);
                return Content("false");
            }
        }



    }
}
