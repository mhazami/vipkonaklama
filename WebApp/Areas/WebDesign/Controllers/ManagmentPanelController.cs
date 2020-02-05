using Radyn.WebApp.AppCode.Security;
using System;
using System.Web.Mvc;
using Radyn.FormGenerator;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;


namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class ManagmentPanelController : WebDesignBaseController
    {


        [RadynAuthorize]
        public ActionResult LoginAsUser(Guid Id)
        {
            var user = WebDesignComponent.Instance.UserFacade.Get(Id);
            if (user == null) return Redirect("~/WebDesign/User/Index");
            SessionParameters.WebDesignUser = user;
            return Redirect("~/WebDesign/UserPanel/Home");
        }



        public ActionResult LookUpUserForm(Guid userId)
        {
            var list =
                FormGeneratorComponent.Instance.WebDesignUserFormsFacade.SelectKeyValuePair(
                    c => c.FormStructure.Id, c => c.FormStructure.Name, x => x.FormStructure.Enable);
            ViewBag.FormList = new SelectList(list, "Key", "Value");
            ViewBag.userId = userId;
            return View();
        }

       
    }
}
