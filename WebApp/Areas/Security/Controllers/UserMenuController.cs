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
    public class UserMenuController : WebDesignBaseController
    {

        [RadynAuthorize]
        public ActionResult Index(Guid userId)
        {

            return View();
        }
        public ActionResult GetUserMenu(Guid userId)
        {

            var Menus =
                SecurityComponent.Instance.UserMenuFacade.Select(x => x.Menu, menu => menu.UserId == userId).OrderByDescending(x => x.TitleWithParentTitle);
            ViewBag.userId = userId;
            return PartialView("PVMenuList", Menus);
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult SearchForNotAdd(Guid UserId, string value)
        {
            var list = SecurityComponent.Instance.MenuFacade.SearchNotAddedInUser(UserId, value);
            return PartialView("PVSearchMenuResult", list);
        }
        [RadynAuthorize]
        public ActionResult AddMenu(Guid UserId)
        {
            ViewBag.UserId = UserId;
            return View();
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult AddMenu(FormCollection collection)
        {
            var UserId = collection["UserId"].ToGuid();
            try
            {
                if (string.IsNullOrEmpty(collection["IdList"]))
                {
                    ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                    return View();
                }
                var strings = collection["IdList"].Split(',');
                var list = strings.Select(Id => Id.ToGuid()).ToList();
                if (SecurityComponent.Instance.UserFacade.AddMenu(UserId, list))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    ViewBag.UserId = UserId;
                    return View();
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                ViewBag.UserId = UserId;
                return View();
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInInsert + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.UserId = UserId;
                return View();
            }
        }

        [RadynAuthorize]
        public ActionResult RemoveMenu(Guid UserId, Guid menuId)
        {
            try
            {
                if (SecurityComponent.Instance.UserMenuFacade.Delete(UserId, menuId))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage);
                    return Redirect("~/Security/UserMenu/Index?userId=" + UserId + "");
                }
                ShowMessage(Resources.Common.ErrorInDelete);
                return Redirect("~/Security/UserMenu/Index?userId=" + UserId + "");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~/Security/UserMenu/Index?userId=" + UserId + "");

            }
        }


    }
}
