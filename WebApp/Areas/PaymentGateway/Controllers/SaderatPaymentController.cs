using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class SaderatPaymentController : WebDesignBaseController
    {

        public ActionResult CallBack(Guid Id, FormCollection collection)
        {

            var melliCallBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (melliCallBackPayRequest == null) return null;
            try
            {
                melliCallBackPayRequest = PaymentGatewayComponenets.Instance.SaderatFacade.SaderatCallBackPayRequest(Id, collection["paymentId"], collection["resultCode"], collection["referenceId"]);
                var message = melliCallBackPayRequest.Status == null
                  ? Resources.Payment.Thereisanerrorinthetransaction
                  : ((SaderatEnums.resultCode)melliCallBackPayRequest.Status).GetDescription();
                return RedirectToAction("GetUrl", "SaderatPayment",
                                    new { message = message, ResCode = melliCallBackPayRequest.Status, Url = melliCallBackPayRequest.LocalUrl, refId = melliCallBackPayRequest.RefId });

            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "SaderatPayment",
                    new { message = exception.Message, ResCode = melliCallBackPayRequest.Status, Url = melliCallBackPayRequest.LocalUrl, refId = melliCallBackPayRequest.RefId });
            }
        }


        public ActionResult Invoice(Guid Id)
       {

            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');
            ViewBag.merchantId = data[0];
            ViewBag.amount = (int)transaction.Amount;
            ViewBag.revertURL = data[5];
            ViewBag.paymentId = data[3];
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
