using System;
using System.Web.Mvc;
using System.Linq;
using Radyn.Security;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.Utility;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class RoleGroupsController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid groupId)
        {
            var roles =
                SecurityComponent.Instance.GroupRoleFacade.Select(x => x.Role, role => role.GroupId == groupId);
            return View(roles);
        }

        [RadynAuthorize]
        public ActionResult Create(Guid groupId)
        {
            var Roles = SecurityComponent.Instance.RoleFacade.GetNotAddedInGroup(groupId);
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection, Guid groupId)
        {
            var roleId = collection["Id"].ToGuid();
            try
            {
                if (SecurityComponent.Instance.GroupFacade.AddRole(roleId, groupId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { groupId = groupId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { groupId = groupId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                var Roles = SecurityComponent.Instance.RoleFacade.GetAll().OrderBy(x => x.Name).ToList();
                ViewBag.Roles = new SelectList(Roles, "Id", "Name");
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveGroup(Guid groupId, Guid roleId)
        {
            return View(SecurityComponent.Instance.GroupFacade.Get(groupId));
        }
        [RadynAuthorize]
        [HttpPost]
        public ActionResult RemoveGroup(FormCollection collection, Guid groupId, Guid roleId)
        {

            try
            {

                if (SecurityComponent.Instance.GroupFacade.RemoveRole(roleId, collection["Id"].ToGuid()))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { groupId = groupId });
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { groupId = groupId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return RedirectToAction("Index", new { groupId = groupId });
            }
        }

    }
}
