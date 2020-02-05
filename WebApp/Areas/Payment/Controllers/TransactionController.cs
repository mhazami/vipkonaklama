using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.FileManager;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using Stimulsoft.Report;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class TransactionController : WebDesignBaseController
    {
        public ActionResult PrintTransaction(Guid Id)
        {

            try
            {
                var model = PaymentComponenets.Instance.TransactionFacade.Get(Id);
                if (model.PayTypeId == (byte)Enums.PayType.Documnet)
                {
                    var file = model.DocScan.HasValue
                        ? FileManagerComponent.Instance.FileFacade.Get(model.DocScan)
                        : null;
                    model.DocScanFile = file != null ? file.Content : null;
                }
                else
                    model.OnlinebankName = model.OnlineBankId.HasValue ?
                        model.OnlineBankId.ToString().ToEnum<Radyn.PaymentGateway.Tools.Enums.Bank>().GetDescription() : "";
                var list =
                PaymentComponenets.Instance.TransactionDiscountFacade.Where(
                    x => x.TransactionId == Id);
                var stiReport1 = new StiReport();
                stiReport1.Load(Server.MapPath(string.Format("~/Areas/Payment/Reports/{0}.mrt", model.PayTypeId == (byte)Enums.PayType.Documnet ? "RptTransactionDocumentResult" : "RptTransactionOnlineResult")));
                stiReport1.RegBusinessObject("Model", model);
                stiReport1.RegBusinessObject("Discount", list);
                SessionParameters.Report = stiReport1;
                return Content("true");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        public ActionResult DetailView(Guid Id)
        {
            System.Web.HttpContext.Current.Session.Remove(Constants.TransactionDiscountAttach);
            var decryptVariables = Extentions.DecryptVariables(Id);
            return View(decryptVariables);
        }
        public ActionResult CallBackToRef(Guid id)
        {
            try
            {
                var decryptVariables = Extentions.DecryptVariables(id);
                var tr = new Transaction { PayDate = decryptVariables.Date };
                var addTransaction = PaymentComponenets.Instance.TransactionFacade.UpdateTempAndAddTransaction(decryptVariables.Id, tr, 0);
                return addTransaction != null ? Redirect("~" + decryptVariables.CallBackUrl) : Redirect("~/Payment/Transaction/DetailView?value=" + id);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~/Payment/Transaction/DetailView?value=" + id);
            }
        }
        public ActionResult GetPayTypes(string value)
        {

            ViewBag.PayTypes = EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.PayType>().Select(
               keyValuePair =>
                   new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.PayType>(),
                       keyValuePair.Value));
            var list = string.IsNullOrEmpty(value) ? null : value.Split('-').ToList();
            var bytes = list != null ? list.Select(variable => variable.ToByte()).ToList() : null;
            return PartialView("PartialViewPaymentTypes", bytes);
        }


        private void GetValue(Temp decryptVariables)
        {
            var list = string.IsNullOrEmpty(decryptVariables.PayType) ? null : decryptVariables.PayType.Split('-').ToList();
            ViewBag.PayTypes = list != null ? list.Select(variable => variable.ToByte()).ToList() : null;
            ViewBag.Accounts = new SelectList(PaymentComponenets.Instance.AccountFacade.SelectKeyValuePair(x => x.Id, x => x.AccountNo + " " + "(" + x.Bank.Title + ")", x => x.IsExternal == false), "Key", "Value");

        }

        public ActionResult View(Guid Id)
        {
            var decryptVariables = Extentions.DecryptVariables(Id);
            GetValue(decryptVariables);
            return View(decryptVariables);
        }
        [HttpPost]
        public ActionResult View(Guid Id, FormCollection collection)
        {
            var decryptVariables = Extentions.DecryptVariables(Id);
            try
            {

                var tr = new Transaction();
                this.RadynTryUpdateModel(tr, collection);
                tr.PayTypeId = (byte)collection["PayTypeId"].ToEnum<Enums.PayType>();
                if (!string.IsNullOrEmpty(collection["PayDate"]))
                    tr.PayDate = DateTime.Parse(DateTimeUtil.ShamsiDateToGregorianDate(collection["PayDate"]).ToString("yyyy-MM-dd ") + collection["PayTime"]);
                HttpPostedFileBase DocScanId = null;
                if (RadynSession["DocScanId"] != null)
                {
                    DocScanId = (HttpPostedFileBase)RadynSession["DocScanId"];
                    RadynSession.Remove("DocScanId");
                }
                if (tr.PayTypeId.Equals((byte)Enums.PayType.Documnet))
                {
                    var documnetPay = PaymentComponenets.Instance.TransactionFacade.DocumnetPay(decryptVariables.Id, tr, DocScanId);
                    if (documnetPay != null)
                    {
                        ShowMessage(Resources.Payment.PaySuccessful, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                        return Redirect("~" + documnetPay.CallBackUrl);
                    }
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return View(decryptVariables);
                }
                if (string.IsNullOrEmpty(collection["bankId"]))
                {
                    ShowMessage(Resources.Payment.PleaseSelectBank, Resources.Common.MessaageTitle,
                          messageIcon: MessageIcon.Error);
                    return View(decryptVariables);
                }
                tr.OnlineBankId = (Byte)collection["bankId"].ToEnum<Radyn.PaymentGateway.Tools.Enums.Bank>();
                string withGateway;
                if (string.IsNullOrEmpty(decryptVariables.TerminalId))
                {
                    withGateway =
                        Radyn.PaymentGateway.PaymentGatewayComponenets.Instance.GeneralFacade.OnlinePay(
                            decryptVariables.Id, tr, System.Web.HttpContext.Current.Request.Url.Authority + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath);
                }
                else
                {
                    withGateway =
                        Radyn.PaymentGateway.PaymentGatewayComponenets.Instance.GeneralFacade.OnlinePay(
                            decryptVariables.Id, tr, System.Web.HttpContext.Current.Request.Url.Authority + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath,
                            decryptVariables.MerchantId, decryptVariables.TerminalId, decryptVariables.TerminalUserName, decryptVariables.TerminalPassword, decryptVariables.CertificatePath, decryptVariables.CertificatePassword, decryptVariables.MerchantPublicKey, decryptVariables.MerchantPrivateKey);
                }
                return Redirect(withGateway);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(decryptVariables);
            }

        }

        public ActionResult SearchTransaction()
        {
            return View();
        }

        public ActionResult SearchTransActionResult(string trackingOrders)
        {
            var lst =
                PaymentComponenets.Instance.TransactionFacade.FirstOrDefault(
                    c => c.TrackYourOrderNum == trackingOrders.ToLong());
            if (lst != null)
            {
                return PartialView("PVTransactionResult", lst);
            }


            return PartialView("PVTemp");


        }

        public ActionResult LookUPTransactionDetails(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult GetTransactionDetails(Guid Id)
        {
            var model = PaymentComponenets.Instance.TransactionFacade.Get(Id);
            ViewBag.Message = model.Done ? Resources.Payment.TransactionDone : Resources.Payment.TransactionFail;
            return PartialView("PVTransactionResult", model);
        }


        public ActionResult GetTransactionDiscount(Guid Id)
        {
            var list =
                PaymentComponenets.Instance.TransactionDiscountFacade.Where(
                    discount => discount.TransactionId == Id);
            return PartialView("PartialViewTransactionDiscount", list);
        }
        public ActionResult GetTempDiscount(Guid Id)
        {
            var list =
                PaymentComponenets.Instance.TempDiscountFacade.Where(
                    discount => discount.TempId == Id);
            return PartialView("PartialViewTempDiscount", list);
        }
        public ActionResult TransactionResult(Guid Id, string callbackurl)
        {
            try
            {

                ViewBag.Id = Id;
                ViewBag.Callbackurl = callbackurl;
                var model = PaymentComponenets.Instance.TransactionFacade.Get(Id);
                ViewBag.MasterMessage = model.PreDone ? Resources.Payment.Paymentwassuccessful : Resources.Payment.PayUnSuccessful;
                ViewBag.Result = model.PreDone;
                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                ViewBag.Callbackurl = callbackurl;
                return View();
            }
        }
        public ActionResult TransactionCallBack(FormCollection collection)
        {
            try
            {

                if (string.IsNullOrEmpty(Request.QueryString["Id"])) return View();
                ViewBag.Message = collection["message"];
                var model = PaymentComponenets.Instance.TransactionFacade.AfterGetway(Request.QueryString["Id"].ToGuid(), collection["refId"], collection["ResCode"], collection["SaleReferenceId"]);
                if (model.Done)
                    return Redirect("~" + model.CallBackUrl);
                return View(model);
            }
            catch (Exception exception)
            {

                ShowExceptionMessage(exception);
                return View();
            }
        }



    }
}
