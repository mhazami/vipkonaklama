using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class UserActionsController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid UserId)
        {
            var Actionss =
                SecurityComponent.Instance.UserActionFacade.Select(x=>x.Action,action => action.UserId == UserId);
            return View(Actionss);
        }

        [RadynAuthorize]
        public ActionResult AddActions(Guid UserId)
        {
            ViewBag.Actions = new SelectList(SecurityComponent.Instance.ActionFacade.GetNotaddedInUser(UserId), "Id", "Name");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult AddActions(FormCollection collection)
        {
            var UserId = Request.QueryString["UserId"].ToGuid();
            
            try
            {
                var ActionsId = collection["Id"].ToGuid();
                if (SecurityComponent.Instance.UserFacade.AddAction(UserId, ActionsId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    return RedirectToAction("Index", new { UserId = Request.QueryString["UserId"] });
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                ViewBag.Actions = new SelectList(SecurityComponent.Instance.ActionFacade.GetNotaddedInUser(UserId), "Id", "Name");

                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Actions = new SelectList(SecurityComponent.Instance.ActionFacade.GetNotaddedInUser(UserId), "Id", "Name");

                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveActions(Guid ActionsId, Guid UserId)
        {
            return View(SecurityComponent.Instance.ActionFacade.Get(ActionsId));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult RemoveActions(FormCollection collection, Guid ActionsId, Guid UserId)
        {
            if (SecurityComponent.Instance.UserActionFacade.Delete(UserId, collection["Id"].ToGuid()))
            {
                ShowMessage(Resources.Common.DeleteSuccessMessage);
                return RedirectToAction("Index", new { UserId = UserId });
            }
            ShowMessage(Resources.Common.ErrorInDelete, messageIcon: MessageIcon.Error);
            return RedirectToAction("Index", new { UserId = UserId });
        }
    }
}
