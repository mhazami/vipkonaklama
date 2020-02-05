using System;
using System.Web.Mvc;
using Radyn.Security;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class UserGroupsController : WebDesignBaseController
    {

        [RadynAuthorize]
        public ActionResult Index(Guid userId)
        {
            var Groups = SecurityComponent.Instance.UserGroupFacade.Select(x=>x.Group,@group => @group.UserId==userId);
            return View(Groups);
        }

        [RadynAuthorize]
        public ActionResult AddGroup(Guid userId)
        {
            ViewBag.Groups = new SelectList(SecurityComponent.Instance.GroupFacade.GetNotAddedInUser(userId), "Id", "Name");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult AddGroup(FormCollection collection)
        {
            var userId = Request.QueryString["userId"].ToGuid();
           
            try
            {
                var GroupId = collection["Id"].ToGuid();
                if (SecurityComponent.Instance.UserFacade.AddGroup(userId, GroupId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    return RedirectToAction("Index", new { userId = Request.QueryString["userId"] });
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                ViewBag.Groups = new SelectList(SecurityComponent.Instance.GroupFacade.GetNotAddedInUser(userId), "Id", "Name");

                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Groups = new SelectList(SecurityComponent.Instance.GroupFacade.GetNotAddedInUser(userId), "Id", "Name");

                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveGroup(Guid GroupId, Guid userId)
        {
            return View(SecurityComponent.Instance.GroupFacade.Get(GroupId));
        }
        [RadynAuthorize]
        
        [HttpPost]
        public ActionResult RemoveGroup(FormCollection collection, Guid userId)
        {
            try
            {
                if (SecurityComponent.Instance.UserGroupFacade.Delete(userId, collection["Id"].ToGuid()))
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
