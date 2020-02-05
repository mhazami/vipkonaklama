using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Payment;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Payment.Controllers
{
    public class PaymentAccountController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = PaymentComponenets.Instance.AccountFacade.Where(x=>x.IsExternal==false);
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
            ViewBag.Bank = new SelectList(CommonComponent.Instance.BankFacade.GetAll(), "Id", "Title");
            return View(new Radyn.Payment.DataStructure.Account());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var account = new Radyn.Payment.DataStructure.Account();
            try
            {
                this.RadynTryUpdateModel(account);
                if (PaymentComponenets.Instance.AccountFacade.Insert(account))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
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
            ViewBag.Bank = new SelectList(CommonComponent.Instance.BankFacade.GetAll(), "Id", "Title");
            return View(PaymentComponenets.Instance.AccountFacade.Get(Id));
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
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
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
            return View(PaymentComponenets.Instance.AccountFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {
            var account = PaymentComponenets.Instance.AccountFacade.Get(Id);
            try
            {
                if (PaymentComponenets.Instance.AccountFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return View(account);
            }
        }



        public ActionResult PaymentAccountDetails(short Id)
        {
            var obj = PaymentComponenets.Instance.AccountFacade.Get(Id);
            return PartialView("PVDetails", obj);
        }
        public ActionResult PaymentAccountModify(short? Id)
        {
            ViewBag.Bank = new SelectList(CommonComponent.Instance.BankFacade.GetAll(), "Id", "Title");
            var obj = Id.HasValue ? PaymentComponenets.Instance.AccountFacade.Get(Id) : new Radyn.Payment.DataStructure.Account();
            return PartialView("PVModify", obj);
        }
    }
}