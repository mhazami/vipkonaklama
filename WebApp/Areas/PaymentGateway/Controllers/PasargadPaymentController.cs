using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class PasargadPaymentController : WebDesignBaseController
    {





        public ActionResult CallBack(Guid Id, FormCollection collection)
        {
            var pasargadCallBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (pasargadCallBackPayRequest == null) return null;
            try
            {
                if (PaymentGatewayComponenets.Instance.PasargadFacade.PasargadCallBackPayRequest(pasargadCallBackPayRequest))
                {
                    return RedirectToAction("GetUrl", "PasargadPayment",
                    new { message = pasargadCallBackPayRequest.OnlinePayMessage, ResCode = pasargadCallBackPayRequest.Status, Url = pasargadCallBackPayRequest.LocalUrl, refId = pasargadCallBackPayRequest.RefId });
                }
                return RedirectToAction("GetUrl", "PasargadPayment",
                   new { message = pasargadCallBackPayRequest.OnlinePayMessage, ResCode = pasargadCallBackPayRequest.Status, Url = pasargadCallBackPayRequest.LocalUrl, refId = pasargadCallBackPayRequest.RefId });

            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "PasargadPayment",
                    new { message = Resources.Payment.Thereisanerrorinthetransaction + "\n" + exception.Message, ResCode = pasargadCallBackPayRequest.Status, Url = pasargadCallBackPayRequest.LocalUrl, refId = pasargadCallBackPayRequest.RefId });
            }
        }



        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');
            ViewBag.merchantCode = data[1];
            ViewBag.terminalCode = data[2];
            ViewBag.amount = data[5];
            ViewBag.redirectAddress = data[6];
            ViewBag.invoiceNumber = data[3];
            ViewBag.invoiceDate = data[4].Replace('+', ' ');
            ViewBag.action = data[7];
            ViewBag.sign = data[9];
            ViewBag.timeStamp = data[8].Replace('+', ' ');
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
