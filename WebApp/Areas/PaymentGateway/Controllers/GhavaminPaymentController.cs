using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class GhavaminPaymentController : WebDesignBaseController
    {

        public ActionResult CallBack(Guid Id, FormCollection collection)
        {

            var callBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);

            if (callBackPayRequest == null) return null;
            try
            {
                callBackPayRequest = PaymentGatewayComponenets.Instance.GhavaminFacade.GhavaminCallBackPayRequest(Id,
                    collection["resultCode"], collection["RRN"], collection["paymentID"]);

                var message = callBackPayRequest.Status == null
                  ? Resources.Payment.Thereisanerrorinthetransaction
                  : GhavaminEnums.VerifyStatusList[callBackPayRequest.Status.ToString()];
                return RedirectToAction("GetUrl", "GhavaminPayment",
                                    new { message = message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });

            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "GhavaminPayment",
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
            ViewBag.paymentTicket = data[2];
            ViewBag.amount = transaction.Amount;
            ViewBag.paymentId = transaction.InvoiceId;
            ViewBag.revertURL = data[3];

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
        //public ActionResult GetUrl(string resultCode, string SayanRef, string paymentID, string Url)
        //{
        //    this.ViewBag.resultCode = resultCode;
        //    this.ViewBag.SayanRef = SayanRef;
        //    this.ViewBag.paymentID = paymentID;
        //    this.ViewBag.url = Url;
        //    return View();

        //}
    }
}
