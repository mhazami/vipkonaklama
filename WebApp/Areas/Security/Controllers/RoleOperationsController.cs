using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class RoleOperationsController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid roleId)
        {
            var operations =SecurityComponent.Instance.RoleOperationFacade.Select(x=>x.Operation,operation => operation.RoleId == roleId);
            return View(operations);
        }

        [RadynAuthorize]
        public ActionResult AddOperation(Guid roleId)
        {
          
            ViewBag.Operation = new SelectList(SecurityComponent.Instance.OperationFacade.GetNotAddedInRole(roleId), "Id", "Title");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult AddOperation(FormCollection collection)
        {
            var roleId = Request.QueryString["roleId"].ToGuid();
            try
            {
                var OperationId = collection["Id"].ToGuid();
                if (SecurityComponent.Instance.RoleFacade.AddOperation(roleId, OperationId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    return RedirectToAction("Index", new { roleId = Request.QueryString["roleId"] });
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                ViewBag.Operation = new SelectList(SecurityComponent.Instance.OperationFacade.GetNotAddedInRole(roleId), "Id", "Title");

                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Operation = new SelectList(SecurityComponent.Instance.OperationFacade.GetNotAddedInRole(roleId), "Id", "Title");

                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveOperation(Guid operationId, Guid roleId)
        {
            return View(SecurityComponent.Instance.OperationFacade.Get(operationId));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult RemoveOperation(FormCollection collection)
        {
            var roleId = Request.QueryString["roleId"].ToGuid();
            if (SecurityComponent.Instance.RoleOperationFacade.Delete(roleId, collection["Id"].ToGuid()))
            {
                ShowMessage(Resources.Common.DeleteSuccessMessage);
                return RedirectToAction("Index", new { roleId = roleId });
            }
            ShowMessage(Resources.Common.ErrorInDelete, messageIcon: MessageIcon.Error);
            return RedirectToAction("Index", new { roleId = roleId });
        }
    }
}
