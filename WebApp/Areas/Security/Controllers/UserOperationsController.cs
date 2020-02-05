using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class UserOperationsController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid userId)
        {
            var Operations =SecurityComponent.Instance.UserOperationFacade.Select(x=>x.Operation,operation => operation.UserId == userId);
            return View(Operations);
        }

        [RadynAuthorize]
        public ActionResult AddOperation(Guid userId)
        {
            ViewBag.Operation = new SelectList(SecurityComponent.Instance.OperationFacade.GetNotAddedInUser(userId), "Id", "Title");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult AddOperation(FormCollection collection)
        {
            var userId = Request.QueryString["userId"].ToGuid();
            try
            {
               
                var OperationId = collection["Id"].ToGuid();
                if (SecurityComponent.Instance.UserFacade.AddOperation(userId, OperationId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    return RedirectToAction("Index", new { userId = Request.QueryString["userId"] });
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                ViewBag.Operation = new SelectList(SecurityComponent.Instance.OperationFacade.GetNotAddedInUser(userId), "Id", "Title");

                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Operation = new SelectList(SecurityComponent.Instance.OperationFacade.GetNotAddedInUser(userId), "Id", "Title");

                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveOperation(Guid operationId, Guid userId)
        {
            return View(SecurityComponent.Instance.OperationFacade.Get(operationId));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult RemoveOperation(FormCollection collection)
        {
            var userId = Request.QueryString["userId"].ToGuid();
            try
            {
               
                if (SecurityComponent.Instance.UserOperationFacade.Delete(userId, collection["Id"].ToGuid()))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage);
                    return RedirectToAction("Index", new { userId = userId });
                }
                ShowMessage(Resources.Common.ErrorInDelete, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { userId = userId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return RedirectToAction("Index", new { userId = userId });
            }
        }

    }
}
