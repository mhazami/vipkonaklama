using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.DataStructure;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.FormGenerator.Tools;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Definition;

namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class UserController : WebDesignBaseController<User>
    {
        [RadynAuthorize]
        public ActionResult Index()
        {
            GetUserSearchValue();
            return View();
        }
        public ActionResult LookUpDetails(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var txtSearch = collection["txtSearch"];
                var status = string.IsNullOrEmpty(collection["SearchStatus"]) ? Enums.UserStatus.None :
                   collection["SearchStatus"].ToEnum<Enums.UserStatus>();
                var gender = string.IsNullOrEmpty(collection["Gender"]) ? Radyn.EnterpriseNode.Tools.Enums.Gender.None : collection["Gender"].ToEnum<Radyn.EnterpriseNode.Tools.Enums.Gender>();
                var user = new User { RegisterDate = collection["RegisterDate"], Status = status };
                var postFormData = this.PostForFormGenerator(collection);
                var list = WebDesignComponent.Instance.UserFacade.SearchDynamic(this.WebSite.Id, txtSearch, user, gender, postFormData);
                GetIndexViewBags(true);
                return PartialView("PartialViewIndex", list);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        private void GetIndexViewBags(bool enableedit = false)
        {
            ViewBag.SearchstatusList =
                EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.UserStatus>().Select(
                    keyValuePair =>
                        new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.UserStatus>(),
                            keyValuePair.Value));
            ViewBag.enableedit = enableedit;
        }
        private void GetUserSearchValue()
        {
            ViewBag.HasValueList =
                EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.HasValue>().Select(
                    keyValuePair =>
                        new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.HasValue>(),
                            keyValuePair.Value));
            ViewBag.SearchstatusList =
             EnumUtils.ConvertEnumToIEnumerableInLocalization<Enums.UserStatus>().Select(
                 keyValuePair =>
                     new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Enums.UserStatus>(),
                         keyValuePair.Value));
            ViewBag.GenderList =
             EnumUtils.ConvertEnumToIEnumerableInLocalization<Radyn.EnterpriseNode.Tools.Enums.Gender>().Select(
                 keyValuePair =>
                     new KeyValuePair<byte, string>((byte)keyValuePair.Key.ToEnum<Radyn.EnterpriseNode.Tools.Enums.Gender>(),
                         keyValuePair.Value));
        }
        [HttpPost]
        public ActionResult UpdateList(FormCollection collection)
        {
            try
            {
                var list = new List<User>();
                var firstOrDefault = collection.AllKeys.FirstOrDefault(s => s.Equals("ModelId"));
                if (!string.IsNullOrEmpty(collection[firstOrDefault]))
                {
                    var strings = collection[firstOrDefault].Split(',');
                    foreach (var key in strings)
                    {
                        if (string.IsNullOrEmpty(key)) continue;
                        var status = collection["Status-" + key.ToGuid()].ToEnum<Enums.UserStatus>();
                        var oldstatus = collection["oldstatus-" + key.ToGuid()].ToEnum<Enums.UserStatus>();
                        if (oldstatus != status)
                        {
                            list.Add(new User()
                            {
                                Id = key.ToGuid(),
                                Status = oldstatus != status ? status : oldstatus,
                            });
                        }
                    }
                }
                var userFacade = WebDesignComponent.Instance.UserFacade;
                if (userFacade.UpdateList(this.WebSite.Id, list))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        [RadynAuthorize]
        public ActionResult ImportFromExcel()
        {
            return View();
        }
        public ActionResult GetImportData()
        {
            HttpPostedFileBase file = null;
            if (RadynSession["Image"] != null)
            {
                file = (HttpPostedFileBase)RadynSession["Image"];
                RadynSession.Remove("Image");
            }
            var importFromExcel = WebDesignComponent.Instance.UserFacade.ImportFromExcel(file, this.WebSite.Id, null);
            RadynSession["UserList"] = importFromExcel;
            return PartialView("PVImportFromExcel", importFromExcel);
        }
        public ActionResult RemoveUser(Guid Id)
        {
            if (RadynSession["UserList"] == null) return Content("false");
            var list = (Dictionary<User, List<string>>)RadynSession["UserList"];
            var any = list.FirstOrDefault(x => x.Key.Id == Id);
            if (any.Key == null) return Content("false");
            list.Remove(any.Key);
            return Content("true");
        }
        [HttpPost]
        public ActionResult ImportFromExcel(FormCollection collection)
        {
            try
            {
                var users = new List<User>();
                if (RadynSession["UserList"] == null) RedirectToAction("Index");
                var list = (Dictionary<User, List<string>>)RadynSession["UserList"];
                var firstOrDefault = collection.AllKeys.FirstOrDefault(s => s.Equals("Checkselect"));
                if (string.IsNullOrEmpty(collection[firstOrDefault]))
                {
                    ShowMessage(Resources.Common.No_results_found, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                    return Redirect("~/WebDesign/User/Index");
                }
                var strings = collection[firstOrDefault].Split(',');
                foreach (var vale in strings)
                {
                    if (string.IsNullOrEmpty(vale)) continue;
                    var orDefault = list.Keys.FirstOrDefault(x => x.Id == vale.ToGuid());
                    if (orDefault != null)
                        users.Add(orDefault);
                }
                if (WebDesignComponent.Instance.UserFacade.InsertList(users))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle,
                                messageIcon: MessageIcon.Succeed);
                    RadynSession.Remove("UserList");
                    return Redirect("~/WebDesign/User/Index");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Redirect("~/WebDesign/User/Index");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View();
            }
        }
        public ActionResult GetModify(Guid Id,PageMode status, bool showpassword = true)
        {
            Radyn.WebDesign.DataStructure.User user = null;
            var prefixTitles = EnterpriseNodeComponent.Instance.PrefixTitleFacade.GetAll();
            SelectList prefixTitleList = null;
            switch (status)
            {
                case PageMode.Edit:
                    user = WebDesignComponent.Instance.UserFacade.Get(Id);
                    prefixTitleList = new SelectList(prefixTitles, "Id", "DescriptionField", user.EnterpriseNode.PrefixTitleId);
                    break;
                case PageMode.Create:
                    user= new User() { EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode() { RealEnterpriseNode = new RealEnterpriseNode() } };
                    prefixTitleList = new SelectList(prefixTitles, "Id", "DescriptionField");
                    break;
            }
            this.TempData.Clear();
            ViewBag.showpassword = showpassword;
            ViewBag.PrefixTitleList = prefixTitleList;
            this.PrepareViewBags(user, status);
            return PartialView("PVModify", user);
        }
        public ActionResult GetDetails(Guid Id, bool viewChild = false)
        {
            ViewBag.viewChild = viewChild;
            return PartialView("PVDetails", WebDesignComponent.Instance.UserFacade.Get(Id));
        }
        [RadynAuthorize]
        public ActionResult Details(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [RadynAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var user = new User(){EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode { RealEnterpriseNode = new RealEnterpriseNode() } };
            try
            {
                this.RadynTryUpdateModel(user);
                this.RadynTryUpdateModel(user.EnterpriseNode);
                this.RadynTryUpdateModel(user.EnterpriseNode.RealEnterpriseNode);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                user.WebId = this.WebSite.Id;
                user.Status = Enums.UserStatus.Register;
                var postFormData = this.PostForFormGenerator(collection);
                if (!string.IsNullOrEmpty(postFormData.FillErrors))
                {
                    ShowMessage(postFormData.FillErrors, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    return Json(new { responseState = false }, JsonRequestBehavior.AllowGet);
                }
                if (WebDesignComponent.Instance.UserFacade.Insert(user, postFormData, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, " Close_thisModal();" }, messageIcon: MessageIcon.Succeed);
                    this.ClearFormGeneratorData(postFormData.Id);
                    return Json(new { responseState = true, userId = user.Id }, JsonRequestBehavior.AllowGet);
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Json(new { responseState = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Json(new { responseState = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [RadynAuthorize]
        public ActionResult Edit(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var Id = collection["Id"].ToGuid();
            var user = WebDesignComponent.Instance.UserFacade.Get(Id);
            try
            {
                this.RadynTryUpdateModel(user);
                this.RadynTryUpdateModel(user.EnterpriseNode);
                this.RadynTryUpdateModel(user.EnterpriseNode.RealEnterpriseNode);
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }

                var postFormData = this.PostForFormGenerator(collection);
                var list = new List<User>();
                var firstOrDefault = collection.AllKeys.FirstOrDefault(s => s.Equals("ModelId"));
                if (!string.IsNullOrEmpty(collection[firstOrDefault]))
                {
                    var strings = collection[firstOrDefault].Split(',');
                    foreach (var key in strings)
                    {
                        if (string.IsNullOrEmpty(key)) continue;
                        var status = collection["Status-" + key.ToGuid()].ToEnum<Enums.UserStatus>();
                        var oldstatus = collection["oldstatus-" + key.ToGuid()].ToEnum<Enums.UserStatus>();
                        if (oldstatus != status)
                        {
                            list.Add(new User()
                            {
                                Id = key.ToGuid(),
                                Status = oldstatus != status ? status : oldstatus,

                            });
                        }

                    }
                }
                if (WebDesignComponent.Instance.UserFacade.Update(user, postFormData, file, list))
                {
                    ShowMessage(Resources.Common.UpdateSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" }, messageIcon: MessageIcon.Succeed);
                    this.ClearFormGeneratorData(postFormData.Id);
                    return Content("true");

                }
                ShowMessage(Resources.Common.ErrorInEdit, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                ViewBag.Id = Id;
                return Content("false");
            }
        }



        public ActionResult DeleteUser(Guid Id)
        {
            try
            {
                if (WebDesignComponent.Instance.UserFacade.Delete(Id))
                {
                    ShowMessage(Resources.Common.DeleteSuccessMessage, Resources.Common.MessaageTitle, new[] { Resources.Common.Ok, "Close_thisModal();" },
                                messageIcon: MessageIcon.Succeed);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInDelete, Resources.Common.MessaageTitle,
                            messageIcon: MessageIcon.Error);
                return Content("false");
            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }
        [HttpPost]
        public ActionResult Validate(FormCollection collection)
        {

            var messageStack = new List<string>();
            var Id = collection["Id"].ToGuid();
            var user = new User();
            var enterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode
            {
                RealEnterpriseNode = new RealEnterpriseNode()
            };
            this.RadynTryUpdateModel(user);
            this.RadynTryUpdateModel(enterpriseNode);
            this.RadynTryUpdateModel(enterpriseNode.RealEnterpriseNode);
            if (Id == Guid.Empty)
            {
                if (string.IsNullOrEmpty(user.Password))
                    messageStack.Add(Resources.WebDesign.Please_Enter_Password);
                if (string.IsNullOrEmpty(collection["RepeatPassword"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_Password_Repeat);
                else if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(collection["RepeatPassword"]))
                    if (user.Password != collection["RepeatPassword"])
                        messageStack.Add(Resources.WebDesign.Password_and_Repeat_Not_Equal);
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    if (string.IsNullOrEmpty(collection["RepeatPassword"]))
                        messageStack.Add(Resources.WebDesign.Please_Enter_Password_Repeat);
                    else if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(collection["RepeatPassword"]))
                        if (user.Password != collection["RepeatPassword"])
                            messageStack.Add(Resources.WebDesign.Password_and_Repeat_Not_Equal);

                }
            }
            if (user.Password != null && !string.IsNullOrEmpty(user.Password) && user.Password.Length < 6)
                messageStack.Add(Resources.WebDesign.MinimumPasswordCharacter);

            if (string.IsNullOrEmpty(enterpriseNode.Email))
                messageStack.Add(Resources.WebDesign.PleaseEnterYourEmail);
            else
            {
                if (!Utility.Utils.IsEmail(enterpriseNode.Email))
                    messageStack.Add(Resources.WebDesign.UnValid_Enter_Email);
            }
            if (string.IsNullOrEmpty(user.Username))
                messageStack.Add(Resources.WebDesign.PleaseInsertUserName);
            if (string.IsNullOrEmpty(enterpriseNode.RealEnterpriseNode.FirstName))
                messageStack.Add(Resources.WebDesign.Please_Enter_YourName);
            if (string.IsNullOrEmpty(enterpriseNode.RealEnterpriseNode.LastName))
                messageStack.Add(Resources.WebDesign.Please_Enter_YourLastName);
            if (string.IsNullOrEmpty(enterpriseNode.Cellphone))
                messageStack.Add(Resources.WebDesign.Please_Enter_YourMobile);
            else
            {
                if (!string.IsNullOrEmpty(enterpriseNode.Cellphone) && ((!enterpriseNode.Cellphone.StartsWith("09") && !enterpriseNode.Cellphone.StartsWith("+") && !enterpriseNode.Cellphone.StartsWith("00")) || ((enterpriseNode.Cellphone.Length < 11) && (enterpriseNode.Cellphone.Length > 15)) || enterpriseNode.Cellphone.ToLong() == 0))
                    messageStack.Add(Resources.WebDesign.MobileNumberIsNotValid);
            }
            if (enterpriseNode.RealEnterpriseNode.Gender == null)
                messageStack.Add(Resources.WebDesign.Please_Enter_YourGender);

            if (!string.IsNullOrEmpty(enterpriseNode.RealEnterpriseNode.NationalCode) && !Radyn.Utility.Utils.ValidNationalID(enterpriseNode.RealEnterpriseNode.NationalCode))
                messageStack.Add(Resources.WebDesign.PleaseEnterRightNationalCode);
            var postFormData = this.PostForFormGenerator(collection);
            if (!string.IsNullOrEmpty(postFormData.FillErrors))
            {
                ShowMessage(postFormData.FillErrors, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Content("false");
            }

            var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
            if (messageBody != "")
            {
                ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Content("false");
            }
            return Content("true");
        }
    }
}