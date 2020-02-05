using System.Linq;
using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class MelliPaymentController : WebDesignBaseController
    {
        public ActionResult CallBack(Guid Id, FormCollection collection)
        {

            var melliCallBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (melliCallBackPayRequest == null) return null;
            try
            {
                melliCallBackPayRequest = PaymentGatewayComponenets.Instance.MelliFacade.MelliCallBackPayRequest(Id, collection["OrderId"], collection["TimeStamp"], collection["FP"]);
                var message = melliCallBackPayRequest.Status == null
                  ? Resources.Payment.Thereisanerrorinthetransaction
                  : ((MelliEnums.AppStatusCode)melliCallBackPayRequest.Status).GetDescription();

                return RedirectToAction("GetUrl", "MelliPayment",
                                    new { message = message, ResCode = melliCallBackPayRequest.Status, Url = melliCallBackPayRequest.LocalUrl, refId = melliCallBackPayRequest.RefId });
            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "MelliPayment",
                    new { message = Resources.Payment.Thereisanerrorinthetransaction + "\n" + exception.Message, ResCode = melliCallBackPayRequest.Status, Url = melliCallBackPayRequest.LocalUrl, refId = melliCallBackPayRequest.RefId });
            }
            return null;
        }


        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var data = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var split = data.Split(',');
            if (split.Count() < 3) return View();
            this.ViewBag.Data = split[3];
            this.ViewBag.token = split[2];
            return this.View();
        }

        public ActionResult GetUrl(string message, string ResCode, string Url, string refId)
        {
            this.ViewBag.resCode = ResCode;
            this.ViewBag.message = message;
            this.ViewBag.url = Url;
            this.ViewBag.refId = refId;
            return this.View();

        }




    }
}
