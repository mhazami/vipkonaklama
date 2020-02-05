using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class EghtesadeNovinPaymentController : WebDesignBaseController
    {

        public ActionResult CallBack(Guid Id, FormCollection collection)
        {
            
            string refrenceNumber = null;
            if (collection["RefNum"] != null)
            {
                refrenceNumber = collection["RefNum"].ToString();
            }

            string cardPanHash = null;
            if (collection["CardPanHash"] != null)
            {
                cardPanHash = collection["CardPanHash"].ToString();
            }

            string transactionState = null;
            if (collection["State"] != null)
            {
                transactionState = collection["State"].ToString();
            }

            var reservationNumber = collection["ResNum"].ToString();
            var merchantId = collection["MID"].ToString();
            var language = collection["language"].ToString();
            var token = collection["token"].ToString();

            //--------------------------

            var bankCallBackPayRequest = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (bankCallBackPayRequest == null) return null;
            try
            {
                bankCallBackPayRequest = PaymentGatewayComponenets.Instance.EghtesadeNovinFacade.EghtesadeNovinCallBackPayRequest(
                    Id, token, merchantId, reservationNumber, refrenceNumber, transactionState, language, cardPanHash);
                var message = (bankCallBackPayRequest.Status == null) ? Resources.Payment.Thereisanerrorinthetransaction : ((SamanEnums.Status)bankCallBackPayRequest.Status).GetDescription();
                return RedirectToAction("GetUrl", "EghtesadeNovinPayment",
                                    new { message = message, ResCode = bankCallBackPayRequest.Status, Url = bankCallBackPayRequest.LocalUrl, refId = bankCallBackPayRequest.RefId });

            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "EghtesadeNovinPayment",
                    new { message = exception.Message, ResCode = bankCallBackPayRequest.Status, Url = bankCallBackPayRequest.LocalUrl, refId = bankCallBackPayRequest.RefId });
            }
        }


        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            var value = !string.IsNullOrEmpty(transaction.AdditionalData) ? StringUtils.Decrypt(transaction.AdditionalData) : "";
            var data = value.Split('#');
            //ViewBag.Amount = (int)transaction.Amount;
            //ViewBag.RedirectURL = data[5];
            //ViewBag.ResNum = data[3];
            ViewBag.Token = data[0];
            ViewBag.Lang = "fa";
            return View();
        }


        public ActionResult GetUrl(string message, string ResCode, string Url, string refId)
        {
            this.ViewBag.resCode = ResCode;
            this.ViewBag.message = message;
            this.ViewBag.url = Url;
            this.ViewBag.refId = refId;
            return View();

        }




    }
}
