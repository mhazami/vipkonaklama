using Radyn.Utility;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Linq;
using System.Web.Mvc;
using Radyn.Help;

namespace Radyn.WebApp.Areas.Help.Controllers
{
    public class HelpMenusController : WebDesignBaseController
    {
        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult Index(Guid helpId)
        {
            ViewBag.helpId = helpId;
            return View();
        }
        public ActionResult GetHelpMenu(Guid helpId)
        {

            var list = HelpComponent.Instance.HelpMenuFacade.Select(x => x.Menu, x => x.HelpId == helpId);
            ViewBag.helpId = helpId;
            return PartialView("PVMenuList", list);
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult AddMenu(Guid helpId)
        {
            ViewBag.helpId = helpId;
            return View();
        }
        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult SearchForNotAdd(Guid helpId, string value)
        {
            
            var list = HelpComponent.Instance.HelpMenuFacade.SearchAddedInHelp(helpId, value);
            return PartialView("PVSearchMenuResult", list);
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        [HttpPost]
        public ActionResult AddMenu(FormCollection collection)
        {
            var opId = collection["helpId"].ToGuid();
            try
            {
                if (string.IsNullOrEmpty(collection["IdList"]))
                {
                    ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                    return View();
                }
                var strings = collection["IdList"].Split(',');
                var list = strings.Select(Id => Id.ToGuid()).ToList();
                if (HelpComponent.Instance.HelpFacade.AddMenu(opId, list))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage);
                    ViewBag.helpId = opId;
                    return View();
                }
                ShowMessage(Resources.Common.GetUnSuccessMessage, messageIcon: MessageIcon.Error);
                return View();
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.helpId = opId;
                return View();
            }
        }

        [RadynAuthorize(AccessLevel = UserType.Host)]
        public ActionResult RemoveMenu(Guid helpId, Guid menuId)
        {
            try
            {
                if (HelpComponent.Instance.HelpMenuFacade.Delete(helpId, menuId))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage);
                    return Redirect("~/Help/HelpMenus/Index?helpId=" + helpId + "");
                }
                ShowMessage(Resources.Common.ErrorInDelete);
                return Redirect("~/Help/HelpMenus/Index?helpId=" + helpId + "");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Redirect("~/Help/HelpMenus/Index?helpId=" + helpId + "");
            }
        }


    }
}
