using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class BankController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.BankFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
        {
            return View(CommonComponent.Instance.BankFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Bank { Enabled = true });
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var bank = new Bank();

            try
            {
                this.RadynTryUpdateModel(bank);
                if (CommonComponent.Instance.BankFacade.Insert(bank))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return RedirectToAction("Index");
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            return View(CommonComponent.Instance.BankFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {

            var bank = CommonComponent.Instance.BankFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(bank);
                if (CommonComponent.Instance.BankFacade.Update(bank))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
               ShowExceptionMessage(exception);
                return View(bank);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            return View(CommonComponent.Instance.BankFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var bank = CommonComponent.Instance.BankFacade.Get(Id);

            try
            {
                if (CommonComponent.Instance.BankFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(bank);
            }
        }
    }
}
