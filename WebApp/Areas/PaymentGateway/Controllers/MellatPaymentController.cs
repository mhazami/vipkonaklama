using Radyn.Payment;
using Radyn.PaymentGateway;
using Radyn.PaymentGateway.Tools;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Base;
using System;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.PaymentGateway.Controllers
{
    public class MellatPaymentController : WebDesignBaseController
    {
        public ActionResult CallBack(Guid Id, FormCollection collection)
        {

            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            try
            {
                 transaction = PaymentGatewayComponenets.Instance.MallatFacade.MellatCallBackPayRequest(Id, collection["ResCode"],
                collection["RefId"], collection["SaleOrderId"].ToLong(), collection["SaleOrderId"].ToLong(),
                collection["SaleReferenceId"].ToLong());
                var message = transaction.Status == null
                    ? Resources.Payment.Thereisanerrorinthetransaction
                    : ((MellatEnums.StatusEnums) transaction.Status).GetDescription();
                return RedirectToAction("GetUrl", "MellatPayment",
                                    new { message = message, ResCode = transaction.Status, Url = transaction.LocalUrl, refId = transaction.RefId, SaleReferenceId = transaction.SaleReferenceId });

            }
            catch (Exception exception)
            {
                return RedirectToAction("GetUrl", "MellatPayment",
                    new { message = Resources.Payment.Thereisanerrorinthetransaction + "\n" + exception.Message, ResCode = transaction.Status, Url = transaction.LocalUrl, refId = transaction.RefId });
            }
        }


        public ActionResult Invoice(Guid Id)
        {
            var transaction = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            if (transaction == null) return null;
            this.ViewBag.refId = transaction.RefId;
            return this.View();
        }


        public ActionResult GetUrl(string message, string ResCode, string Url, string refId, string SaleReferenceId)
        {
            this.ViewBag.resCode = ResCode;
            this.ViewBag.message = message;
            this.ViewBag.url = Url;
            this.ViewBag.refId = refId;
            this.ViewBag.SaleReferenceId = SaleReferenceId;
            return this.View();

        }




    }
}
