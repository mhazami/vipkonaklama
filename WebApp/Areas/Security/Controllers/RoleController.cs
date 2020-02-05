using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class RoleController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = SecurityComponent.Instance.RoleFacade.GetAll();
            return View(list);
        }

       
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(SecurityComponent.Instance.RoleFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Role());
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var role = new Role();
            try
            {
                this.RadynTryUpdateModel(role, collection);
                if (SecurityComponent.Instance.RoleFacade.Insert(role))
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
                return View(role);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            return View(SecurityComponent.Instance.RoleFacade.Get(Id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var role = SecurityComponent.Instance.RoleFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(role, collection);
                if (SecurityComponent.Instance.RoleFacade.Update(role))
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
                return View(role);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(SecurityComponent.Instance.RoleFacade.Get(Id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var role = SecurityComponent.Instance.RoleFacade.Get(Id);
            try
            {
                if (SecurityComponent.Instance.RoleFacade.Delete(Id))
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
                return View(role);
            }
        }
    }
}
