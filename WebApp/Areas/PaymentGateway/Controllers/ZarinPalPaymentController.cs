using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;


namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class ZarinPalPaymentController : WebDesignBaseController
    {

        public ActionResult CallBack(Guid Id)
        {
            var callBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (callBackPayRequest == null) return null;
            try
            {
                if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
                {
                    callBackPayRequest = PaymentGatewayComponenets.Instance.ZarinPalFacade.ZarinPalCallBackPayRequest(Id, Request.QueryString["Status"], Request.QueryString["Authority"]);


                    var message = callBackPayRequest.Status == null
                ? Resources.Payment.Thereisanerrorinthetransaction
                : ZarinPalEnums.VerifyStatusList[callBackPayRequest.Status.ToString()];
                    return RedirectToAction("GetUrl", "ZarinPalPayment",
                                        new { message = message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });
                }
            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "ZarinPalPayment",
                 new { message = exception.Message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });
            }
            return null;
        }

        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');

            ViewBag.amount = transaction.Amount;
            ViewBag.paymentId = transaction.InvoiceId;
            ViewBag.authority = data[3];

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
