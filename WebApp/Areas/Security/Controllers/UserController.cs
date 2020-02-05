using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.Security;
using Radyn.Security.DataStructure;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Mvc.UI.Captcha;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;



namespace Radyn.WebApp.Areas.Security.Controllers
{
    public class UserController : WebDesignBaseController
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            var users = SecurityComponent.Instance.UserFacade.GetAllUserInfo();
            return View(users);
        }

        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }


        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Validate(FormCollection collection)
        {

            var messageStack = new List<string>();
            var newPassword = collection["NewPassword"];
            var newPasswordRepeat = collection["RepeatNewPassword"];
            var oldPassword = collection["OldPassword"];
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newPasswordRepeat) || string.IsNullOrEmpty(oldPassword))
                messageStack.Add(Resources.Common.Please_complete_Form_Data);
            else
            {
                if (!SecurityComponent.Instance.UserFacade.CheckOldPassword(SessionParameters.User.Id, oldPassword))
                    messageStack.Add("OldPasswordIsWrong");
                if (newPassword != newPasswordRepeat)
                    messageStack.Add("Password_and_Repeat_Not_Equal");

            }
            var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
            if (messageBody != "")
            {
                ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Content("false");
            }
            return Content("true");

        }


        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {

            try
            {
                var newPassword = collection["NewPassword"];
                var oldPassword = collection["OldPassword"];
                if (SecurityComponent.Instance.UserFacade.ChangePassword(SessionParameters.User.Username, oldPassword, newPassword))
                {
                    SessionParameters.User = null;
                    ShowMessage("", "",
                                   new[] { Resources.Common.Ok, " window.location='" + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/Security/User/Login'; " });
                    return Content("true");
                }
                ShowMessage(Resources.Common.No_results_found);
                return Content("false");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return Content("false");
            }

        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View(new User());
        }
        public ActionResult GenerateUser(string state, Guid id)
        {
            var objectState = state.ToEnum<Radyn.Common.Definition.ObjectState>();
            User user = null;
            switch (objectState)
            {
                case Radyn.Common.Definition.ObjectState.Create:
                    user = new User();
                    break;
                case Radyn.Common.Definition.ObjectState.Edit:
                    user =
                        SecurityComponent.Instance.UserFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.Details:
                    break;
                case Radyn.Common.Definition.ObjectState.Delete:
                    user =
                        SecurityComponent.Instance.UserFacade.Get(id);
                    break;
                case Radyn.Common.Definition.ObjectState.List:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state");
            }
            return PartialView("PartialViewUser", user);
        }
        [HttpPost]
        [RadynAuthorize]
        public ActionResult Create(FormCollection collection)
        {
            var user = new User();
            try
            {
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                this.RadynTryUpdateModel(user, collection);
                var EnterPrise = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode { RealEnterpriseNode = new RealEnterpriseNode() };
                this.RadynTryUpdateModel(EnterPrise.RealEnterpriseNode, collection);

                if (SecurityComponent.Instance.UserFacade.Insert(user, EnterPrise, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index", new { UserId = Request.QueryString["UserId"] });
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View(user);
            }
        }
        public ActionResult GetModify(Guid? Id)
        {
            var model = Id.HasValue ? SecurityComponent.Instance.UserFacade.Get(Id) :
                new User()
                { EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode() { RealEnterpriseNode = new RealEnterpriseNode() } };
            ViewBag.State = Id.HasValue ? "Edit" : "Create";
            return PartialView("PVModify", model);
        }

        public ActionResult GetDetails(Guid Id)
        {
            return PartialView("PVDetails", SecurityComponent.Instance.UserFacade.Get(Id));
        }
        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            var user = SecurityComponent.Instance.UserFacade.Get(Id);
            try
            {
                var ENobj = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade.Get(Id);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                if (string.IsNullOrEmpty(collection["Password"])) collection["Password"] = "";


                this.TryUpdateModel(user, collection);
                this.TryUpdateModel(ENobj, collection);
                this.TryUpdateModel(ENobj.RealEnterpriseNode, collection);
                if (SecurityComponent.Instance.UserFacade.Update(user, ENobj, file))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowMessage(Resources.Common.ErrorInEdit + exception.Message, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.Id = Id;
                return View(user);
            }
        }

        [RadynAuthorize]
        public ActionResult Delete(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        [RadynAuthorize]
        public ActionResult Delete(Guid Id, FormCollection collection)
        {
            try
            {
                if (SecurityComponent.Instance.UserFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    return RedirectToAction("Index");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return View();
            }
        }


        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session.Remove("user");
            System.Web.HttpContext.Current.Session.Remove("Operations");
            System.Web.HttpContext.Current.Session.Remove("UserType");
            SessionParameters.HasLoginPasswordError = false;
            Session.Abandon();
            return View(DateTime.Now);
        }

        [HttpPost]
        [RadynAuthorize(DoAuthorize = false)]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(FormCollection collection)
        {
            try
            {

                if (!Request.Url.Authority.Contains("localhost") && SessionParameters.HasLoginPasswordError)
                {
                    var service = new CaptchaService();
                    if (!service.IsValidCaptcha(collection["captch"]))
                    {
                        SessionParameters.HasLoginPasswordError = true;
                        throw new Exception(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                    }
                }
                var userName = collection["username"];
                var password = collection["password"];
                if (FormsAuthentication.Authenticate(userName, StringUtils.Encrypt(password)))
                {

                    FormsAuthentication.SetAuthCookie(userName, false);
                    switch (userName.ToLower())
                    {
                        case "host":
                            SessionParameters.UserType = UserType.Host;
                            SessionParameters.UserOperation = SecurityComponent.Instance.OperationFacade.GetAll();
                            break;

                    }
                    return !string.IsNullOrEmpty(Request.QueryString["returnUrl"])
                        ? this.Redirect(Request.QueryString["returnUrl"])
                        : this.Redirect("~/Account/Index");
                }
                var user = SecurityComponent.Instance.UserFacade.Login(userName, password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    SessionParameters.User = user;
                    SessionParameters.UserOperation = SecurityComponent.Instance.OperationFacade.GetAllByUserId(user.Id);
                    SessionParameters.UserType = UserType.User;
                    return !string.IsNullOrEmpty(Request.QueryString["returnUrl"])
                               ? this.Redirect(Request.QueryString["returnUrl"])
                               : this.Redirect("~/Account/Index");
                }
                SessionParameters.HasLoginPasswordError = true;
                ShowMessage(Resources.Security.Please_enter_the_correct_values,
                             messageIcon: MessageIcon.Security);
                ViewBag.Username = userName;
                return View();
            }
            catch (Exception ex)
            {
                SessionParameters.HasLoginPasswordError = true;
                ShowExceptionMessage(ex);
            }
            return View();
        }

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            System.Web.HttpContext.Current.Session.Remove("user");
            System.Web.HttpContext.Current.Session.Remove("Operations");
            System.Web.HttpContext.Current.Session.Remove("UserType");
            SessionParameters.HasLoginPasswordError = false;
            Session.Abandon();
            return Redirect("~/Security/User/Login");
        }

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult GenerateMenu()
        {
            if (!IsAuthenticated || SessionParameters.UserOperation == null)
                return Content("");
            var operations = SessionParameters.UserOperation;
            return PartialView("PartialMainMenu", operations);
        }

        [RadynAuthorize(DoAuthorize = false)]
        public ActionResult GetOperationMenu(string operationId)
        {

            if (!IsAuthenticated || SessionParameters.UserOperation == null)
                return Content("");
            var firstOrDefault = SessionParameters.UserOperation.FirstOrDefault(x => x.Id.Equals(operationId.ToGuid()));
            if (firstOrDefault == null)
                return Content("");
            FillOperationMenuGroup(firstOrDefault);
            if (firstOrDefault.MenuGroupList.Any())
            {
                ViewBag.OpId = operationId;
                return PartialView("PartialMainGroupItem", firstOrDefault.MenuGroupList);
            }
            OperationMenuFacade(firstOrDefault);
            ViewBag.MenuElementId = operationId;
            return PartialView("PartialMainMenuItem", firstOrDefault.AllMenus);




        }



        public ActionResult GetOperationMenuNoGroup(string operationId)
        {
            if (!IsAuthenticated || SessionParameters.UserOperation == null)
                return Content("");
            var firstOrDefault = SessionParameters.UserOperation.FirstOrDefault(x => x.Id.Equals(operationId.ToGuid()));
            if (firstOrDefault == null)
                return Content("");
            OperationMenuFacade(firstOrDefault);
            ViewBag.MenuElementId = operationId;
            return PartialView("PartialMainMenuItem", firstOrDefault.AllMenus.Where(x => x.MenuGroupId == null));
        }
        public ActionResult GetOperationMenuByGroup(string operationId, int groupId)
        {
            if (!IsAuthenticated || SessionParameters.UserOperation == null)
                return Content("");
            var firstOrDefault = SessionParameters.UserOperation.FirstOrDefault(x => x.Id.Equals(operationId.ToGuid()));
            if (firstOrDefault == null)
                return Content("");
            OperationMenuFacade(firstOrDefault);
            ViewBag.MenuElementId = operationId + "-" + groupId;
            return PartialView("PartialMainMenuItem", firstOrDefault.AllMenus.Where(x => x.MenuGroupId == groupId));
        }
        [RadynAuthorize]
        public ActionResult GetMenuChilds(string MenuId)
        {
            var mid = MenuId.ToGuid();
            var operationMenus = SecurityComponent.Instance.MenuFacade.GetChildMenu(SessionParameters.User.Id, mid);
            ViewBag.MenuElementId = MenuId;
            return PartialView("PartialMainMenuItem", operationMenus);
        }

        [RadynAuthorize]
        public ActionResult GenerateChildMenu(IEnumerable<Menu> child)
        {
            return PartialView("PartialChildMenuItem", child);
        }

        public ActionResult Menu(Guid oid)
        {
            if (!IsAuthenticated || SessionParameters.UserOperation == null)
                return Redirect("/security/User/Login");
            var firstOrDefault = SessionParameters.UserOperation.FirstOrDefault(x => x.Id.Equals(oid));
            if (firstOrDefault != null)
            {
                FillOperationMenuGroup(firstOrDefault);
                ViewBag.OpTitle = firstOrDefault.Title;
                if (firstOrDefault.MenuGroupList.Any())
                    return Redirect("/security/User/MenuGroup?oid=" + firstOrDefault.Id);

                FillOperationMenu(firstOrDefault);
                if (firstOrDefault.ParentMenus.Count() == 1) return Redirect(firstOrDefault.ParentMenus.First().Url);

            }
            return View(firstOrDefault != null ? firstOrDefault.ParentMenus : new List<dynamic>());
        }


        public ActionResult MenuGroup(Guid oid)
        {
            if (!IsAuthenticated || SessionParameters.UserOperation == null)
                return Redirect("/security/User/Login");
            var firstOrDefault = SessionParameters.UserOperation.FirstOrDefault(x => x.Id.Equals(oid));

            if (firstOrDefault != null)
            {
                FillOperationMenuGroup(firstOrDefault);
                ViewBag.OpTitle = firstOrDefault.Title;
                if (firstOrDefault.MenuGroupList.Any())
                {
                    var orDefault = firstOrDefault.MenuGroupList.FirstOrDefault();
                    if (orDefault != null) ViewBag.GroupMenuId = orDefault.Id;
                }
                if (firstOrDefault.MenuGroupList.Count() == 1)
                {
                    int? id = firstOrDefault.MenuGroupList.FirstOrDefault().Id;
                    var menus = SecurityComponent.Instance.MenuFacade.Select(x => x.Url, menu => menu.Enabled && menu.Display && menu.ParentId == null && menu.MenuGroupId == id);
                    if (menus.Count == 1) return Redirect(menus.First());
                }
            }
            ViewBag.OpId = firstOrDefault != null ? firstOrDefault.Id : (Guid?)null;
            if (firstOrDefault != null) return View(firstOrDefault.MenuGroupList);

            return View(new List<dynamic>());
        }

        private void FillOperationMenu(Operation firstOrDefault)
        {
            if (firstOrDefault.ParentMenus == null)
            {
                firstOrDefault.ParentMenus =
                    SecurityComponent.Instance.OperationMenuFacade.Select(
                        new Expression<Func<OperationMenu, object>>[]
                        {
                            x => x.Menu.Url, x => x.Menu.Title,
                            x => x.Menu.ImageId,
                            x => x.Menu.ParentId,
                            x => x.Menu.Display,
                            x => x.Menu.Enabled
                        },
                        menu => menu.OperationId == firstOrDefault.Id && menu.Menu.ParentId == null && menu.Menu.Display);
            }
        }

        private void FillOperationMenuGroup(Operation firstOrDefault)
        {
            if (firstOrDefault.MenuGroupList == null)
                firstOrDefault.MenuGroupList = SecurityComponent.Instance.MenuGroupFacade.Select(new Expression<Func<MenuGroup, object>>[] { x => x.Name, x => x.ImageId, x => x.Id, x => x.OperationId }, menu => menu.OperationId == firstOrDefault.Id && menu.Enabled);

        }
        private void OperationMenuFacade(Operation operation)
        {
            var operationMenuFacade = SecurityComponent.Instance.OperationMenuFacade;
            if (operation.AllMenus == null)
            {
                if (operation.AllMenus == null)
                    operation.AllMenus = new List<Menu>();
                operation.AllMenus = operationMenuFacade.GetOprationMenu(operation.Id,
                    SessionParameters.User != null ? SessionParameters.User.Id : (Guid?)null);


            }
        }
        private static void FillOprationMenuGroupMenulist(int groupId, Operation firstOrDefault)
        {
            if (firstOrDefault.GroupMenuList == null)
                firstOrDefault.GroupMenuList = new Dictionary<int, List<Menu>>();
            if (!firstOrDefault.GroupMenuList.ContainsKey(groupId))
            {
                var menulist =
                    SecurityComponent.Instance.MenuFacade.OrderBy(x => x.Order,
                        x => x.MenuGroupId == groupId && !x.ParentId.HasValue && x.Display && x.Enabled);
                firstOrDefault.GroupMenuList.Add(groupId, menulist);
            }
        }
        public ActionResult GetMenuByGroup(int groupId, Guid? OpId = null)
        {
            var list = new List<Menu>();
            if (OpId != null)
            {
                var firstOrDefault = SessionParameters.UserOperation.FirstOrDefault(x => x.Id.Equals(OpId));
                if (firstOrDefault == null) return PartialView("PartialMenuByGroup", list);
                {
                    FillOprationMenuGroupMenulist(groupId, firstOrDefault);
                    list = firstOrDefault.GroupMenuList[groupId];
                }
            }
            else
            {
                list =
           SecurityComponent.Instance.MenuFacade.OrderBy(x => x.Order,
               x => x.MenuGroupId == groupId && !x.ParentId.HasValue && x.Display && x.Enabled);
            }


            return PartialView("PartialMenuByGroup", list);
        }


    }
}
