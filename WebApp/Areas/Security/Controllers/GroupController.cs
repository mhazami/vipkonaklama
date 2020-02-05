using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class GroupController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var list = SecurityComponent.Instance.GroupFacade.GetAll();
            return View(list);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            return View(SecurityComponent.Instance.GroupFacade.Get(Id));
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new Group());
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var group = new Group();
            try
            {
                this.RadynTryUpdateModel(group, collection);
                group.Id = Guid.NewGuid();
                if (SecurityComponent.Instance.GroupFacade.Insert(group))
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
                return View(group);
            }
        }

        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            return View(SecurityComponent.Instance.GroupFacade.Get(Id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var group = SecurityComponent.Instance.GroupFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(group, collection);
                if (SecurityComponent.Instance.GroupFacade.Update(group))
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
                return View(group);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            return View(SecurityComponent.Instance.GroupFacade.Get(Id));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var group = SecurityComponent.Instance.GroupFacade.Get(Id);
            try
            {
                if (SecurityComponent.Instance.GroupFacade.Delete(Id))
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
                return View(group);
            }
        }
    }
}
