using System;
using System.Web;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class OperationController : WebDesignBaseController
    {
        [RadynAuthorize()]
        public ActionResult Index()
        {
            var list = SecurityComponent.Instance.OperationFacade.GetAll();
            return View(list);
        }

       

        [RadynAuthorize()]
        public ActionResult Details(Guid Id)
        {
            return View(SecurityComponent.Instance.OperationFacade.Get(Id));
        }

        [RadynAuthorize()]
        public ActionResult Create()
        {
            return View(new Operation());
        }

        [RadynAuthorize()]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var operation = new Operation();
            try
            {
                this.RadynTryUpdateModel(operation,collection);
                operation.Id = Guid.NewGuid();
                HttpPostedFileBase fileBase = null;
                if (RadynSession["OprationIcon"] != null)
                {
                    fileBase = (HttpPostedFileBase)RadynSession["OprationIcon"];
                    RadynSession.Remove("OprationIcon");
                }
                if (SecurityComponent.Instance.OperationFacade.Insert(operation,fileBase))
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
                return View(operation);
            }
        }

        [RadynAuthorize()]
        public ActionResult Edit(Guid Id)
        {
            return View(SecurityComponent.Instance.OperationFacade.Get(Id));
        }

        [RadynAuthorize()]
        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var operation = SecurityComponent.Instance.OperationFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(operation, collection);
                HttpPostedFileBase fileBase = null;
                if (RadynSession["OprationIcon"] != null)
                {
                    fileBase = (HttpPostedFileBase)RadynSession["OprationIcon"];
                    RadynSession.Remove("OprationIcon");
                }
                if (SecurityComponent.Instance.OperationFacade.Update(operation, fileBase))
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
                return View(operation);
            }
        }

        [RadynAuthorize()]
        public ActionResult Delete(Guid Id)
        {
            return View(SecurityComponent.Instance.OperationFacade.Get(Id));
        }

        [RadynAuthorize()]
        [HttpPost]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            var operation = SecurityComponent.Instance.OperationFacade.Get(Id);
            try
            {
                if (SecurityComponent.Instance.OperationFacade.Delete(Id))
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
