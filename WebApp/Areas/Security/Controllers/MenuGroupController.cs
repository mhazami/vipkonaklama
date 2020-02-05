using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class MenuGroupController : WebDesignBaseController
    {
        [RadynAuthorize()]
        public ActionResult Index()
        {
            var list = SecurityComponent.Instance.MenuGroupFacade.GetAll();
            return View(list);
        }

       

        [RadynAuthorize()]
        public ActionResult Details(Int32 id)
        {
            var detail = SecurityComponent.Instance.MenuGroupFacade.Get(id);
            return View(detail);
        }

        [RadynAuthorize()]
        public ActionResult Create()
        {
            ViewBag.Operations=new SelectList(SecurityComponent.Instance.OperationFacade.GetAll(),"Id","Title");
            return View(new MenuGroup());
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult GetOperationMenu(Guid operationId,int? groupId)
        {
            ViewBag.GroupId = groupId;
            var list = SecurityComponent.Instance.MenuFacade.GetByOprationIdParentIsnull(operationId);
            return PartialView("PVSearchMenuResult", list);
        }
        [RadynAuthorize()]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var operation = new MenuGroup();
            try
            {
                this.RadynTryUpdateModel(operation,collection);
                var list=new List<Guid>();
                var value = collection["IdList"];
                if (!string.IsNullOrEmpty(value))
                {
                    list.AddRange(value.Split(',').Select(variable => variable.ToGuid()));
                }
                HttpPostedFileBase fileBase = null;
                if (RadynSession["MenuGroupIcon"] != null)
                {
                    fileBase = (HttpPostedFileBase)RadynSession["MenuGroupIcon"];
                    RadynSession.Remove("MenuGroupIcon");
                }
                if (SecurityComponent.Instance.MenuGroupFacade.Insert(operation, list,fileBase))
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
                ViewBag.Operations = new SelectList(SecurityComponent.Instance.OperationFacade.GetAll(), "Id", "Title");
                return View(operation);
            }
        }

        [RadynAuthorize()]
        public ActionResult Edit(int Id)
        {
            ViewBag.Operations = new SelectList(SecurityComponent.Instance.OperationFacade.GetAll(), "Id", "Title");
            return View(SecurityComponent.Instance.MenuGroupFacade.Get(Id));
        }

        [RadynAuthorize()]
        [HttpPost]
        public ActionResult Edit(int Id, FormCollection collection)
        {
            var operation = SecurityComponent.Instance.MenuGroupFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(operation, collection);
                var list = new List<Guid>();
                var value = collection["IdList"];
                if (!string.IsNullOrEmpty(value))
                {
                    list.AddRange(value.Split(',').Select(variable => variable.ToGuid()));
                }
                HttpPostedFileBase fileBase = null;
                if (RadynSession["MenuGroupIcon"] != null)
                {
                    fileBase = (HttpPostedFileBase)RadynSession["MenuGroupIcon"];
                    RadynSession.Remove("MenuGroupIcon");
                }
                if (SecurityComponent.Instance.MenuGroupFacade.Update(operation, list,fileBase))
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
                ViewBag.Operations = new SelectList(SecurityComponent.Instance.OperationFacade.GetAll(), "Id", "Title");
                return View(operation);
            }
        }

        [RadynAuthorize()]
        public ActionResult Delete(int Id)
        {
            return View(SecurityComponent.Instance.MenuGroupFacade.Get(Id));
        }

        [RadynAuthorize()]
        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            var operation = SecurityComponent.Instance.MenuGroupFacade.Get(Id);
            try
            {
                if (SecurityComponent.Instance.MenuGroupFacade.Delete(Id))
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
                return View(operation);
            }
        }
      
    }
}
