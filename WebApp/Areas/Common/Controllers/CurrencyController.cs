using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using Radyn.Common.DataStructure;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class CurrencyController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.CurrencyFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int16 Id)
        {
            return View(CommonComponent.Instance.CurrencyFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Currency());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var currency = new Currency();
            try
            {
                this.RadynTryUpdateModel(currency);
               if (CommonComponent.Instance.CurrencyFacade.Insert(currency))
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
                return View(currency);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Int16 Id)
        {
            return View(CommonComponent.Instance.CurrencyFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Int16 Id, FormCollection collection)
        {
            var currency = CommonComponent.Instance.CurrencyFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(currency);
                if (CommonComponent.Instance.CurrencyFacade.Update(currency))
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
                return View(currency);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int16 Id)
        {
            return View(CommonComponent.Instance.CurrencyFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Int16 Id, FormCollection collection)
        {
            var currency = CommonComponent.Instance.CurrencyFacade.Get(Id);
            try
            {
                if (CommonComponent.Instance.CurrencyFacade.Delete(Id))
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
                return View(currency);
            }
        }
    }
}