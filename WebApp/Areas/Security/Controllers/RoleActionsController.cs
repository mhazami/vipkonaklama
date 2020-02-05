using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class RoleActionsController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid RoleId)
        {
            var actions =
                SecurityComponent.Instance.RoleActionFacade.Select(x=>x.Action,action => action.RoleId == RoleId);
            return View(actions);
        }

        [RadynAuthorize]
        public ActionResult AddActions(Guid RoleId)
        {
            ViewBag.Actions = new SelectList(SecurityComponent.Instance.ActionFacade.GetNotaddedInRole(RoleId), "Id", "Name");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult AddActions(FormCollection collection)
        {
            var RoleId = Request.QueryString["RoleId"].ToGuid();

            try
            {
                var ActionsId = collection["Id"].ToGuid();
                if (SecurityComponent.Instance.RoleActionFacade.AddAction(RoleId, ActionsId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    return RedirectToAction("Index", new { UserId = Request.QueryString["RoleId"] });
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);

                ViewBag.Actions = new SelectList(SecurityComponent.Instance.ActionFacade.GetNotaddedInRole(RoleId), "Id", "Name");

                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Actions = new SelectList(SecurityComponent.Instance.ActionFacade.GetNotaddedInRole(RoleId), "Id", "Name");


                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveActions(Guid ActionsId, Guid RoleId)
        {
            return View(SecurityComponent.Instance.ActionFacade.Get(ActionsId));
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult RemoveActions(FormCollection collection, Guid ActionsId, Guid RoleId)
        {
            if (SecurityComponent.Instance.RoleActionFacade.Delete(RoleId, collection["Id"].ToGuid()))
            {
                ShowMessage(Resources.Common.DeleteSuccessMessage);
                return RedirectToAction("Index", new { RoleId = RoleId });
            }
            ShowMessage(Resources.Common.ErrorInDelete, messageIcon: MessageIcon.Error);
            return RedirectToAction("Index", new { RoleId = RoleId });
        }
    }
}
