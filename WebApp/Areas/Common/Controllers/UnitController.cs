using System;
using System.Web.Mvc;
using Radyn.Common;
using Radyn.Common.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Common.Controllers
{
    public class UnitController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = CommonComponent.Instance.UnitFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Int32 Id)
        {
            return View(CommonComponent.Instance.UnitFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Unit());
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var unit = new Unit();
            try
            {
                this.RadynTryUpdateModel(unit);
                if (CommonComponent.Instance.UnitFacade.Insert(unit))
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
                return View(unit);
            }
        }

        [RadynAuthorize]
        [RadynAuthorize]
        public ActionResult Edit(Int32 Id)
        {
            return View(CommonComponent.Instance.UnitFacade.Get(Id));
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Int32 Id, FormCollection collection)
        {
            var unit = CommonComponent.Instance.UnitFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(unit);
                if (CommonComponent.Instance.UnitFacade.Update(unit))
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
                return View(unit);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Int32 Id)
        {
            return View(CommonComponent.Instance.UnitFacade.Get(Id));
        }

        [HttpPost]
        public ActionResult Delete(Int32 Id, FormCollection collection)
        {
            var unit = CommonComponent.Instance.UnitFacade.Get(Id);
            try
            {
                if (CommonComponent.Instance.UnitFacade.Delete(Id))
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
                return View(unit);
            }
        }
    }
}