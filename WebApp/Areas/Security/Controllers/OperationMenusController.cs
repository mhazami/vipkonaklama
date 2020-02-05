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
    public class OperationMenusController : WebDesignBaseController
    {
        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Index(Guid operationId)
        {
            ViewBag.OperationId = operationId;
            return View();
        }
        public ActionResult GetOprationMenu(Guid operationId)
        {

            var list = SecurityComponent.Instance.OperationMenuFacade.Select(x => x.Menu, x => x.OperationId == operationId);
            ViewBag.OperationId = operationId;
            return PartialView("PVMenuList", list);
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult AddMenu(Guid operationId)
        {
            ViewBag.OperationId = operationId;
            return View();
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult SearchForNotAdd(Guid operationId, string value)
        {
            var list = SecurityComponent.Instance.MenuFacade.SearchNotAddedInOpration(operationId, value);
            return PartialView("PVSearchMenuResult", list);
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult AddMenu(FormCollection collection)
        {
            var opId = collection["operationId"].ToGuid();
            try
            {
                if (string.IsNullOrEmpty(collection["IdList"]))
                {
                    ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                    return View();
                }
                var strings = collection["IdList"].Split(',');
                var list = strings.Select(Id => Id.ToGuid()).ToList();
                if (SecurityComponent.Instance.OperationFacade.AddMenu(opId, list))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    ViewBag.OperationId = opId;
                    return View();
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.OperationId = opId;
                return View();
            }
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult RemoveMenu(Guid operationId, Guid menuId)
        {
            try
            {
                if (SecurityComponent.Instance.OperationMenuFacade.Delete(operationId, menuId))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage);
                    return Redirect("~/Security/OperationMenus/Index?operationId=" + operationId + "");
                }
                ShowMessage(Resources.Common.ErrorInDelete);
                return Redirect("~/Security/OperationMenus/Index?operationId=" + operationId + "");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~/Security/OperationMenus/Index?operationId=" + operationId + "");
            }
        }


    }
}
