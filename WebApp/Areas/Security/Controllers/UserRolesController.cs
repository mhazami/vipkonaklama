using System;
using System.Web.Mvc;
using Radyn.Security.DataStructure;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class UserRolesController : WebDesignBaseController
    {

        [RadynAuthorize]
        public ActionResult Index(Guid userId)
        {
            //نقش هایی که کاربر دارد
            var userRoles =
                Radyn.Security.SecurityComponent.Instance.UserRoleFacade.Select(x=>x.Role,role => role.UserId == userId);
            return View(userRoles);
        }

        [RadynAuthorize]
        public ActionResult Create(Guid UserId)
        {


            ViewBag.Roles = new SelectList(Radyn.Security.SecurityComponent.Instance.RoleFacade.GetNotAddedInUser(UserId), "Id", "Name");
            return View();


        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection, Guid UserId)
        {
            var role = new Role();
            try
            {
                this.RadynTryUpdateModel(role, collection);
                if (Radyn.Security.SecurityComponent.Instance.UserFacade.AddRole(role.Id, UserId))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index", new { UserId = UserId });
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { UserId = UserId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Roles = new SelectList(Radyn.Security.SecurityComponent.Instance.RoleFacade.GetNotAddedInUser(UserId), "Id", "Name");
               
                return View(role);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid roleId)
        {
            var role = Radyn.Security.SecurityComponent.Instance.RoleFacade.Get(roleId);
            return View(role);
        }

        [RadynAuthorize]
        [HttpPost]
        public ActionResult Delete(Guid roleId, Guid UserId)
        {
            try
            {
                Radyn.Security.SecurityComponent.Instance.UserRoleFacade.Delete(UserId, roleId);
                return RedirectToAction("Index", new { UserId = UserId });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return RedirectToAction("Index", new { userId = UserId });
            }
        }

    }
}
