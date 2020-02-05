using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class ActionController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid menuId)
        {
            var list = SecurityComponent.Instance.ActionFacade.Where(action => action.MenuId == menuId);
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id, Guid menuId)
        {
            return View(SecurityComponent.Instance.ActionFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create(Guid menuId)
        {

            return View(new Radyn.Security.DataStructure.Action());
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(Guid menuId, FormCollection collection)
        {
            var action = new Radyn.Security.DataStructure.Action { Id = Guid.NewGuid(), MenuId = menuId, Name = collection["Name"] };
            try
            {
                if (SecurityComponent.Instance.ActionFacade.Insert(action))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { menuId = menuId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { menuId = menuId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(action);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id, Guid menuId)
        {
            ViewBag.Menu = new SelectList(SecurityComponent.Instance.MenuFacade.GetAll(), "Id", "Title", menuId);
            return View(SecurityComponent.Instance.ActionFacade.Get(Id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid Id, Guid menuId, FormCollection collection)
        {
            var action = SecurityComponent.Instance.ActionFacade.Get(Id);
            try
            {
                action.MenuId = menuId;
                action.Name = collection["Name"];
                if (SecurityComponent.Instance.ActionFacade.Update(action))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { menuId = action.MenuId });
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { menuId = action.MenuId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(action);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(SecurityComponent.Instance.ActionFacade.Get(Id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid Id, Guid menuId, FormCollection collection)
        {
            var action = SecurityComponent.Instance.ActionFacade.Get(Id);
            try
            {
                if (SecurityComponent.Instance.ActionFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { menuId = menuId });
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(action);
            }
        }
    }
}
