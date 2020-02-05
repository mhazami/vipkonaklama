using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Payment;
using Radyn.Payment.DataStructure;
using Radyn.Payment.Tools;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class WebDesignAccountController : WebDesignBaseController
    {
        // GET: WebDesign/Account
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PaymentComponenets.Instance.WebDesignAccountFacade.Select(x => x.PaymentAccount, x => x.WebId == this.WebSite.Id);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int16 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var account = new Radyn.Payment.DataStructure.Account();
            try
            {
                this.RadynTryUpdateModel(account);
                if (PaymentComponenets.Instance.WebDesignAccountFacade.Insert(this.WebSite.Id, account))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return this.SubmitRedirect(collection, new { Id = account.Id });

                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/Payment/WebDesignAccount/");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Bank = new SelectList(CommonComponent.Instance.BankFacade.GetAll(), "Id", "Title");
                return View(account);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var account = PaymentComponenets.Instance.AccountFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(account);
                if (PaymentComponenets.Instance.AccountFacade.Update(account))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return this.SubmitRedirect(collection, new { Id = Id });

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/Payment/WebDesignAccount/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(account);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {

            try
            {
                if (PaymentComponenets.Instance.WebDesignAccountFacade.Delete(this.WebSite.Id, Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Redirect("~/Payment/WebDesignAccount/Index");

                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Redirect("~/Payment/WebDesignAccount/Index");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View();
            }
        }

        [WebDesignUserAuthorize]
        public ActionResult Payment(Guid Id)
        {
            var decryptVariables = Extentions.DecryptVariables(Id);
            try
            {
                decryptVariables.AdditionalData = WebDesignComponent.Instance.ConfigurationFacade.FillTempAdditionalData(this.WebSite.Id);
                PaymentComponenets.Instance.TempFacade.Update(decryptVariables);
                GetValue(decryptVariables);
                return View(decryptVariables);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                GetValue(decryptVariables);

                return View();
            }
        }
        [WebDesignUserAuthorize]
        [HttpPost]
        public ActionResult Payment(Guid Id, FormCollection collection)
        {
            var decryptVariables = Extentions.DecryptVariables(Id);
            try
            {

                var tr = new Transaction();
                this.RadynTryUpdateModel(tr, collection);
                tr.PayTypeId = (byte)collection["PayTypeId"].ToEnum<Radyn.Payment.Tools.Enums.PayType>();
                if (!string.IsNullOrEmpty(collection["PayDate"]))
                    tr.PayDate = DateTime.Parse(DateTimeUtil.ShamsiDateToGregorianDate(collection["PayDate"]).ToString("yyyy-MM-dd ") + collection["PayTime"]);


                HttpPostedFileBase DocScanId = null;
                if (RadynSession["DocScanId"] != null)
                {
                    DocScanId = (HttpPostedFileBase)RadynSession["DocScanId"];
                    RadynSession.Remove("DocScanId");
                }
                if (tr.PayTypeId.Equals((byte)Radyn.Payment.Tools.Enums.PayType.Documnet))
                {
                    var documnetPay = PaymentComponenets.Instance.TransactionFacade.DocumnetPay(decryptVariables.Id, tr, DocScanId);
                    if (documnetPay != null)
                    {


                        return Redirect("~" + documnetPay.CallBackUrl);
                    }
                    ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    GetValue(decryptVariables);
                    return View(decryptVariables);
                }

                if (string.IsNullOrEmpty(collection["bankId"]))
                {
                    ShowMessage(Resources.Payment.PleaseSelectBank, Resources.Common.MessaageTitle,
                          messageIcon: MessageIcon.Error);
                    GetValue(decryptVariables);
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
                GetValue(decryptVariables);
                return View(decryptVariables);
            }

        }
        private void GetValue(Temp decryptVariables)
        {
            var list = string.IsNullOrEmpty(decryptVariables.PayType) ? null : decryptVariables.PayType.Split('-').ToList();
            ViewBag.PayTypes = list != null ? list.Select(variable => variable.ToByte()).ToList() : null;
            //            ViewBag.Accounts = new SelectList(WebDesignComponent.Instance.AccountFacade.SelectKeyValuePair(x => x.AccountId, x => x.Account.AccountNo + " " + "(" + x.Account.Bank.Title + ")", x => x.CongressId == this.Homa.Id), "Key", "Value");


        }
    }
}