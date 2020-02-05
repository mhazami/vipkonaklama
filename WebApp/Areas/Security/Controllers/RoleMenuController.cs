using Radyn.Security;
using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class RoleMenuController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index(Guid roleId)
        {

            return View();
        }
        public ActionResult GetUserMenu(Guid roleId)
        {

            var Menus =
                SecurityComponent.Instance.RoleMenuFacade.Select(x => x.Menu, x => x.RoleId == roleId);

            ViewBag.roleId = roleId;
            return PartialView("PVMenuList", Menus);
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult SearchForNotAdd(Guid roleId, string value)
        {
            var list = SecurityComponent.Instance.MenuFacade.SearchNotAddedInRole(roleId, value);
            return PartialView("PVSearchMenuResult", list);
        }
        [RadynAuthorize]
        public ActionResult AddMenu(Guid roleId)
        {
            ViewBag.roleId = roleId;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult AddMenu(FormCollection collection)
        {
            var roleId = collection["roleId"].ToGuid();
            try
            {
                if (string.IsNullOrEmpty(collection["IdList"]))
                {
                    ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                    ViewBag.roleId = roleId;
                    return View();
                }
                var strings = collection["IdList"].Split(',');
                var list = strings.Select(Id => Id.ToGuid()).ToList();
                if (SecurityComponent.Instance.RoleFacade.AddMenu(roleId, list))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    ViewBag.roleId = roleId;
                    return View();
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                ViewBag.roleId = roleId;
                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.roleId = roleId;
                return View();
            }
        }


        public ActionResult RemoveMenu(Guid roleId, Guid menuId)
        {
            try
            {
                if (SecurityComponent.Instance.RoleMenuFacade.Delete(roleId, menuId))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage);
                    return Redirect("~/Security/RoleMenu/Index?roleId=" + roleId + "");
                }
                ShowMessage(Resources.Common.ErrorInDelete);
                return Redirect("~/Security/RoleMenu/Index?roleId=" + roleId + "");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~/Security/RoleMenu/Index?roleId=" + roleId + "");

            }
        }


    }
}
