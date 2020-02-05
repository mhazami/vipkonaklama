using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class IranKishPaymentController : WebDesignBaseController
    {

        public ActionResult CallBack(Guid Id, FormCollection collection)
        {
            var callBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (callBackPayRequest == null) return null;
            try
            {
                callBackPayRequest = PaymentGatewayComponenets.Instance.IranKishFacade.IranKishCallBackPayRequest(Id,"", collection["resultCode"], collection["referenceId"]);
                var message = callBackPayRequest.Status == null
                  ? Resources.Payment.Thereisanerrorinthetransaction
                  : IranKishEnums.VerifyStatusList[callBackPayRequest.Status.ToString()];
                return RedirectToAction("GetUrl", "IranKishPayment",
                                    new { message = message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });

            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "IranKishPayment",
                    new { message = exception.Message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });
            }
        }


        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');
            ViewBag.token = data[0]; //token
            ViewBag.merchantId = data[1];//MerchandId
            return View();
        }


        public ActionResult GetUrl(string message, string ResCode, string Url, string refId)
        {
            this.ViewBag.resCode = ResCode;
            this.ViewBag.message = message;
            this.ViewBag.url = Url;
            this.ViewBag.refId = refId; //referencedId
            return View();

        }




    }
}
