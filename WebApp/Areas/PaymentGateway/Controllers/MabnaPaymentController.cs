using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class MabnaPaymentController : WebDesignBaseController
    {

        public ActionResult CallBack(Guid Id, FormCollection collection)
        {
            var resCode = collection["RESCODE"];
            var trn = collection["TRN"];
            var crn = collection["CRN"];
            var amount = (collection["AMOUNT"] != null) ? System.Convert.ToInt32(collection["AMOUNT"]) : 0;

            

            var callBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (callBackPayRequest == null) return null;
            try
            {
                callBackPayRequest = PaymentGatewayComponenets.Instance.MabnaFacade.MabnaCallBackPayRequest(Id, resCode, trn, crn, amount);
                var message = callBackPayRequest.Status == null
                  ? Resources.Payment.Thereisanerrorinthetransaction
                  : MabnaEnums.VerifyStatusList[callBackPayRequest.Status.ToString()];
                return RedirectToAction("GetUrl", "MabnaPayment", new { message = message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });
            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "MabnaPayment", new { message =  exception.Message, ResCode = callBackPayRequest.Status, Url = callBackPayRequest.LocalUrl, refId = callBackPayRequest.RefId });
            }
        }


        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');
            ViewBag.MID = data[0]; //MerchandId
            ViewBag.TID = data[1]; //TerminalId
            ViewBag.ResNum = data[2]; // = paymentId
            ViewBag.Amount = (int)transaction.Amount;//data[3]
            ViewBag.RedirectURL = data[4]; //revert URL
            ViewBag.Token = data[5]; //Token Key

            return View();
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
