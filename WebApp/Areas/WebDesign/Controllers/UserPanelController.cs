using Radyn.EnterpriseNode.DataStructure;
using Radyn.Utility;
using Radyn.Web.Html;
using Radyn.Web.Mvc.UI.Captcha;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Security;
using Radyn.WebApp.Areas.FormGenerator.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Radyn.FormGenerator;
using Radyn.FormGenerator.Tools;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebDesign;
using Radyn.WebDesign.DataStructure;
using Enums = Radyn.WebDesign.Definition.Enums;

namespace Radyn.WebApp.Areas.WebDesign.Controllers
{
    public class UserPanelController : WebDesignBaseController
    {

        public ActionResult Login()
        {
            if (this.WebSite.Status != Enums.WebSiteStatus.NoProblem)
                return Redirect("/Home/Index");
            if (SessionParameters.WebDesignUser != null)
                return Redirect("~/WebDesign/UserPanel/Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                if (Request.Url != null && !Request.Url.Authority.Contains("localhost") && SessionParameters.HasLoginPasswordError)
                {
                    if (collection.AllKeys.Contains("captch"))
                    {
                        var service = new CaptchaService();
                        if (!service.IsValidCaptcha(collection["captch"]))
                        {
                            SessionParameters.HasLoginPasswordError = true;
                            throw new Exception(Resources.Security.CapchaIsNotCorrect);

                        }
                    }
                }
                var userName = collection["UserName"];
                var password = collection["password"];
                var user = WebDesignComponent.Instance.UserFacade.Login(userName, password, this.WebSite.Id);
                if (user != null && user.Status != Enums.UserStatus.PreRegister)
                {
                    SessionParameters.WebDesignUser = user;
                    return !string.IsNullOrEmpty(Request.QueryString["returnUrl"])
                              ? this.RadynRedirect(Request.QueryString["returnUrl"])
                              : this.RadynRedirect("~/WebDesign/UserPanel/Home");

                }
                SessionParameters.HasLoginPasswordError = true;
                ShowMessage(Resources.Security.Please_enter_the_correct_values,messageIcon: MessageIcon.Security);
                ViewBag.userName = userName;
                this.Redirect("~/WebDesign/UserPanel/Login");
            }
            catch (Exception ex)
            {
                SessionParameters.HasLoginPasswordError = true;
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
            }
            return this.Redirect("~/WebDesign/UserPanel/Login");
        }

        [WebDesignUserAuthorize]
        public ActionResult EditInfoUser()
        {

            return View();
        }








        [HttpPost]
        public ActionResult ModifyUser(FormCollection collection)
        {

            try
            {
                var messageStack = new List<string>();
                var id = collection["Id"].ToGuid();
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];
                    RadynSession.Remove("Image");
                }
                var user = WebDesignComponent.Instance.UserFacade.Get(id);

                this.RadynTryUpdateModel(user);
                this.RadynTryUpdateModel(user.EnterpriseNode);
                this.RadynTryUpdateModel(user.EnterpriseNode.RealEnterpriseNode);
                if (SessionParameters.WebDesignUser.Username != user.Username)
                {
                    if (string.IsNullOrEmpty(user.Username))
                        messageStack.Add(Resources.WebDesign.PleaseInsertUserName);

                }
                if (string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.FirstName))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourName);
                if (string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.LastName))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourLastName);
                if (string.IsNullOrEmpty(user.EnterpriseNode.Cellphone))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourMobile);
                var postFormData = this.PostForFormGenerator(collection);
                if (!string.IsNullOrEmpty(postFormData.FillErrors))
                {
                    messageStack.Add(postFormData.FillErrors);
                }
                var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
                if (messageBody != "")
                {
                    ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    return Content("false");
                }
                user.Password = String.Empty;
                if (WebDesignComponent.Instance.UserFacade.Update(user, postFormData, file))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    SessionParameters.WebDesignUser = WebDesignComponent.Instance.UserFacade.Get(user.Id);
                    this.ClearFormGeneratorData(postFormData.Id);
                    return Content("true");
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return Content("false");
            }
        }

        [WebDesignUserAuthorize]
        public ActionResult Home()
        {
            var list =
                FormGeneratorComponent.Instance.WebDesignUserFormsFacade.SelectKeyValuePair(
                    c => c.FormStructure.Id, c => c.FormStructure.Name);
            ViewBag.FormList = list;
            return View();
        }



        [WebDesignUserAuthorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            var newPassword = collection["NewPassword"];
            if (WebDesignComponent.Instance.UserFacade.ChangePassword(SessionParameters.WebDesignUser.Id, newPassword))
            {
                ShowMessage(Resources.WebDesign.PasswordSuccedChanged, "",
                               new[] { Resources.Common.Ok, " window.location='" + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Home'; " });
                return Content("true");
            }
            ShowMessage(Resources.Common.No_results_found);
            return Content("false");
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
                if (!WebDesignComponent.Instance.UserFacade.CheckOldPassword(SessionParameters.WebDesignUser.Id, oldPassword))
                    messageStack.Add(Resources.WebDesign.OldPasswordIsWrong);
                if (newPassword != newPasswordRepeat)
                    messageStack.Add(Resources.WebDesign.Password_and_Repeat_Not_Equal);
            }
            var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
            if (messageBody != "")
            {
                ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Content("false");
            }
            return Content("true");

        }
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session.Remove("WebDesignUser");
            Session.Abandon();
            SessionParameters.HasLoginPasswordError = false;
            return Redirect("~/WebDesign/UserPanel/Login");
        }

        public ActionResult UploadPaymentPhoto(IEnumerable<HttpPostedFileBase> fileBase)
        {
            var file = Request.Files["UploadPaymentPhoto"];
            if (file != null)
            {
                if (file.InputStream != null)
                {

                    RadynSession["ImagePayment"] = file;
                }
            }
            return Content("");
        }
        public ActionResult Subscribe()
        {
            if (SessionParameters.WebDesignUser != null)
                return Redirect("~/WebDesign/UserPanel/Home");
            return View();
        }

        [HttpPost]
        [RadynAuthorize(DoAuthorize = false)]
        [ValidateAntiForgeryToken()]
        public ActionResult Subscribe(FormCollection collection)
        {
            try
            {

                var messageStack = new List<string>();
                if (!Request.Url.Authority.Contains("localhost"))
                {
                    var service = new CaptchaService();
                    if (!service.IsValidCaptcha(collection["captch"]))
                        messageStack.Add(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                }
                if (string.IsNullOrEmpty(collection["name"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourName);
                if (string.IsNullOrEmpty(collection["lastname"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourLastName);
                if (string.IsNullOrEmpty(collection["mail"]))
                    messageStack.Add(Resources.WebDesign.PleaseEnterYourEmail);
                else
                {

                    if (!Utility.Utils.IsEmail(collection["mail"]))
                        messageStack.Add(Resources.WebDesign.UnValid_Enter_Email);
                }
                var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
                if (messageBody != "")
                {
                    ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    ViewBag.Name = collection["name"];
                    ViewBag.LastName = collection["lastname"];
                    ViewBag.Email = collection["mail"];
                    return View();
                }
                var config = this.WebSite.Configuration;
                if (config.RegisterEmailConfirm)
                {
                    if (Request.UrlReferrer != null)
                    {
                        var status = WebDesignComponent.Instance.UserFacade.Register(this.WebSite.Id, collection["mail"],
                            collection["name"], collection["lastname"], "http://" + System.Web.HttpContext.Current.Request.Url.Authority + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Complete", SessionParameters.Culture);
                        switch (status)
                        {
                            case Enums.SubscribeStatus.None:
                                break;
                            case Enums.SubscribeStatus.MailSent:
                                ShowMessage(
                                    Resources.WebDesign.MailSentMessage,
                                     Resources.WebDesign.EmailRegisterInCongress, messageIcon: MessageIcon.Succeed);
                                break;
                            case Enums.SubscribeStatus.NotConfirmed:
                                ShowMessage(
                                   Resources.WebDesign.NotConfirmedMessage + "<br/><br/>" +
                                   " <div style='float:left;'><a href='javascript:;' onclick=sendMail('" + collection["mail"] +
                                   "')>" + Resources.WebDesign.Anotherlinkinactivation + "</a></div>",
                                   Resources.WebDesign.EmailRegisterInCongress, messageIcon: MessageIcon.Warning);
                                break;
                            case Enums.SubscribeStatus.Confirmed:
                                break;
                            case Enums.SubscribeStatus.Registered:
                                ShowMessage(Resources.WebDesign.YouhavealreadyregisteredCongress, Resources.WebDesign.EmailRegisterInCongress, messageIcon: MessageIcon.Warning);
                                break;
                            case Enums.SubscribeStatus.Deactived:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    return Redirect("~/WebDesign/UserPanel/Subscribe");

                }
                var model = WebDesignComponent.Instance.UserFacade.FirstOrDefault(x => x.EnterpriseNode.Email == collection["mail"] && x.WebId == this.WebSite.Id);
                if (model == null)
                {
                    if (WebDesignComponent.Instance.UserFacade.RegisterWithOutSendMail(this.WebSite.Id, collection["mail"], collection["name"], collection["lastname"]))
                        return Redirect("~/WebDesign/UserPanel/CompleteWithOutEmail?email=" + collection["mail"]);
                }
                else
                {
                    if (model.Status == Enums.UserStatus.PreRegister)
                        return Redirect("~/WebDesign/UserPanel/CompleteWithOutEmail?email=" + collection["mail"]);
                    ShowMessage(Resources.WebDesign.YouhavealreadyregisteredCongress, Resources.WebDesign.EmailRegisterInCongress, messageIcon: MessageIcon.Warning);
                    return Redirect("~/WebDesign/UserPanel/Subscribe");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ShowMessage(ex.Message + " " + ex.InnerException.Message, "", messageIcon: MessageIcon.Security);
                else
                    ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
            }
            return Redirect("~/WebDesign/UserPanel/Subscribe");
        }





        public ActionResult SendConfirmLink(string mail)
        {
            var model = WebDesignComponent.Instance.UserFacade.FirstOrDefault(x => x.EnterpriseNode.Email == mail && x.WebId ==
                 this.WebSite.Id);
            if (model == null)
            {
                return Content("false");
            }
            if (Request.UrlReferrer != null && WebDesignComponent.Instance.UserFacade.SendConfirmLink(mail, "http://" + System.Web.HttpContext.Current.Request.Url.Authority + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Complete", this.WebSite.Id, model.DescriptionField))
            {
                ShowMessage(Resources.WebDesign.MailSentMessage,
                                  Resources.WebDesign.EmailRegisterInCongress, messageIcon: MessageIcon.Succeed);
            }
            return Content("");
        }
        public ActionResult CompleteWithOutEmail()
        {
            return PartialView("PVCompleteWithOutEmail");
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult CompleteWithOutEmail(FormCollection collection)
        {
            try
            {
                var userFacade = WebDesignComponent.Instance.UserFacade;
                var messageStack = new List<string>();
                var user = new User();
                user.EnterpriseNode = new Radyn.EnterpriseNode.DataStructure.EnterpriseNode();
                user.EnterpriseNode.RealEnterpriseNode = new RealEnterpriseNode();
                this.RadynTryUpdateModel(user);
                this.RadynTryUpdateModel(user.EnterpriseNode);
                this.RadynTryUpdateModel(user.EnterpriseNode.RealEnterpriseNode);
                user.WebId = this.WebSite.Id;
                user.Status = Enums.UserStatus.Register;
                HttpPostedFileBase file = null;
                if (RadynSession["Image"] != null)
                {
                    file = (HttpPostedFileBase)RadynSession["Image"];

                }
                var service = new CaptchaService();
                if (!service.IsValidCaptcha(collection["captch"]))
                    messageStack.Add(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                if (string.IsNullOrEmpty(collection["Password"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_Password);
                if (string.IsNullOrEmpty(collection["RepeatPassword"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_Password_Repeat);
                if (user.Password != null && !string.IsNullOrEmpty(user.Password) && user.Password.Length < 6)
                    messageStack.Add(Resources.WebDesign.MinimumPasswordCharacter);
                if (string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.FirstName))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourName);
                if (string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.LastName))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourLastName);
                if (string.IsNullOrEmpty(user.EnterpriseNode.Cellphone))
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourMobile);
                else if (!string.IsNullOrEmpty(user.EnterpriseNode.Cellphone) && ((!user.EnterpriseNode.Cellphone.StartsWith("09") && !user.EnterpriseNode.Cellphone.StartsWith("+") && !user.EnterpriseNode.Cellphone.StartsWith("00")) || ((user.EnterpriseNode.Cellphone.Length < 11) && (user.EnterpriseNode.Cellphone.Length > 15)) || user.EnterpriseNode.Cellphone.ToLong() == 0))
                    messageStack.Add(Resources.WebDesign.MobileNumberIsNotValid);
                if (user.EnterpriseNode.RealEnterpriseNode.Gender == null)
                    messageStack.Add(Resources.WebDesign.Please_Enter_YourGender);
                if (collection["Password"] != collection["RepeatPassword"])
                    messageStack.Add(Resources.WebDesign.Password_and_Repeat_Not_Equal);
                var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
                if (messageBody != "")
                {
                    ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    return Content("false");
                }

                var postFormData = this.PostForFormGenerator(collection);
                if (!string.IsNullOrEmpty(postFormData.FillErrors))
                {
                    ShowMessage(postFormData.FillErrors, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Warning);
                    this.ClearFormGeneratorData(postFormData.Id);
                    return Content("false");
                }
                if (userFacade.Insert(user, postFormData, file))
                {
                    RadynSession.Remove("Image");
                    var login = userFacade.Login(user.Username, collection["Password"], this.WebSite.Id);
                    if (login != null)
                    {
                        FormsAuthentication.SetAuthCookie(collection["UserName"], false);
                        SessionParameters.WebDesignUser = login;
                        ShowMessage(Resources.WebDesign.CompleteRegisterMessage, "",
                            new[] { Resources.Common.Ok, " window.location='" + Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Home'; " });
                        return Content("true");

                    }
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
                return Content("false");
            }

        }


        public ActionResult Complete(Guid Id)
        {

            var model = WebDesignComponent.Instance.UserFacade.Get(Id);
            if (model == null)
            {
                ShowMessage(Resources.WebDesign.YourUserNameDeletedPleaseRegisterAgain, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Redirect("~/WebDesign/UserPanel/Subscribe");
            }
            if (model.Status != Enums.UserStatus.PreRegister)
                return Redirect("~/WebDesign/UserPanel/Login");
            ViewBag.Id = model.Id;
            return View();
        }

        [HttpPost]
        public ActionResult Complete(FormCollection collection)
        {
            try
            {
                var userFacade = WebDesignComponent.Instance.UserFacade;
                var messageStack = new List<string>();
                var user = userFacade.Get(collection["Id"].ToGuid());
                this.RadynTryUpdateModel(user);
                if (user != null)
                {

                    this.RadynTryUpdateModel(user.EnterpriseNode);
                    this.RadynTryUpdateModel(user.EnterpriseNode.RealEnterpriseNode);
                    HttpPostedFileBase file = null;
                    if (RadynSession["Image"] != null)
                        file = (HttpPostedFileBase)RadynSession["Image"];

                    var service = new CaptchaService();
                    if (!service.IsValidCaptcha(collection["captch"]))
                        messageStack.Add(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                    if (string.IsNullOrEmpty(collection["Password"]))
                        messageStack.Add(Resources.WebDesign.Please_Enter_Password);
                    if (string.IsNullOrEmpty(collection["RepeatPassword"]))
                        messageStack.Add(Resources.WebDesign.Please_Enter_Password_Repeat);
                    if (user.Password != null && !string.IsNullOrEmpty(user.Password) && user.Password.Length < 6)
                        messageStack.Add(Resources.WebDesign.MinimumPasswordCharacter);
                    if (string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.FirstName))
                        messageStack.Add(Resources.WebDesign.Please_Enter_YourName);
                    if (string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.LastName))
                        messageStack.Add(Resources.WebDesign.Please_Enter_YourLastName);
                    if (user.EnterpriseNode.RealEnterpriseNode.NationalCode == null || string.IsNullOrEmpty(user.EnterpriseNode.RealEnterpriseNode.NationalCode.Trim()))
                        messageStack.Add(Resources.WebDesign.Please_Enter_NationalCode);
                    if (string.IsNullOrEmpty(user.EnterpriseNode.Cellphone))
                        messageStack.Add(Resources.WebDesign.Please_Enter_YourMobile);
                    else if (!string.IsNullOrEmpty(user.EnterpriseNode.Cellphone) && ((!user.EnterpriseNode.Cellphone.StartsWith("09") && !user.EnterpriseNode.Cellphone.StartsWith("+") && !user.EnterpriseNode.Cellphone.StartsWith("00")) || ((user.EnterpriseNode.Cellphone.Length < 11) && (user.EnterpriseNode.Cellphone.Length > 15)) || user.EnterpriseNode.Cellphone.ToLong() == 0))
                        messageStack.Add(Resources.WebDesign.MobileNumberIsNotValid);
                    if (user.EnterpriseNode.RealEnterpriseNode.Gender == null)
                        messageStack.Add(Resources.WebDesign.Please_Enter_YourGender);
                    if (collection["Password"] != collection["RepeatPassword"])
                        messageStack.Add(Resources.WebDesign.Password_and_Repeat_Not_Equal);
                    var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
                    if (messageBody != "")
                    {
                        ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                        return Content("false");
                    }

                    var postFormData = this.PostForFormGenerator(collection);
                    if (!string.IsNullOrEmpty(postFormData.FillErrors))
                    {
                        ShowMessage(postFormData.FillErrors, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Warning);
                        this.ClearFormGeneratorData(postFormData.Id);
                        return Content("false");
                    }
                    if (userFacade.CompleteRegister(user, postFormData, file))
                    {
                        RadynSession.Remove("Image");
                        var login = userFacade.Login(user.Username, collection["Password"], this.WebSite.Id);
                        if (login != null)
                        {
                            FormsAuthentication.SetAuthCookie(collection["UserName"], false);
                            SessionParameters.WebDesignUser = login;
                            ShowMessage(Resources.WebDesign.CompleteRegisterMessage, "",
                                new[] { Resources.Common.Ok, " window.location='" + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/Home'; " });
                            return Content("true");


                        }


                    }
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                return Content("false");

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
                return Content("false");
            }

        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [RadynAuthorize(DoAuthorize = false)]
        //[ValidateAntiForgeryToken(Salt = "radynAntiFor")]
        public ActionResult ForgotPassword(FormCollection collection)
        {
            try
            {
                if (!Request.Url.Authority.Contains("localhost"))
                {
                    var service = new CaptchaService();
                    if (!service.IsValidCaptcha(collection["captch"]))
                        throw new Exception(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                }
                var userName = collection["username"];
                if (Request.UrlReferrer != null && WebDesignComponent.Instance.UserFacade.ForgotPassword(userName, "http://" + Request.UrlReferrer.Authority + Radyn.Web.Mvc.UI.Application.CurrentApplicationPath + "/WebDesign/UserPanel/NewPassword", this.WebSite.Id))
                {
                    ShowMessage(Resources.Security.Your_New_Password_Sent_Email);
                }
                ViewBag.Username = userName;
                return View();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
            }
            return View();
        }
        public ActionResult NewPassword(Guid Id)
        {

            var user = WebDesignComponent.Instance.UserFacade.Get(Id);
            if (user == null)
            {
                ShowMessage(Resources.WebDesign.YourUserNameDeletedPleaseRegisterAgain, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                return Redirect("~/WebDesign/UserPanel/Subscribe");
            }
            return View(user);
        }

        [HttpPost]
        [RadynAuthorize(DoAuthorize = false)]
        // [ValidateAntiForgeryToken(Salt = "radynAntiFor")]
        public ActionResult NewPassword(Guid Id, FormCollection collection)
        {
            User user = null;
            try
            {
                if (!Request.Url.Authority.Contains("localhost"))
                {
                    var service = new CaptchaService();
                    if (!service.IsValidCaptcha(collection["captch"]))
                        throw new Exception(Resources.CommonComponent.Enterthesecuritycodeisnotvalid);
                }

                user = WebDesignComponent.Instance.UserFacade.Get(Id);
                if (user == null)
                {
                    ShowMessage(Resources.WebDesign.YourUserNameDeletedPleaseRegisterAgain, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    return Redirect("~/WebDesign/UserPanel/Subscribe");
                }
                var messageStack = new List<string>();
                if (string.IsNullOrEmpty(collection["Password"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_Password);
                if (string.IsNullOrEmpty(collection["RepeatPassword"]))
                    messageStack.Add(Resources.WebDesign.Please_Enter_Password_Repeat);
                else if (collection["Password"] != collection["RepeatPassword"])
                    messageStack.Add(Resources.WebDesign.Password_and_Repeat_Not_Equal);
                var messageBody = messageStack.Aggregate("", (current, item) => current + Tag.Li(item));
                if (messageBody != "")
                {
                    ShowMessage(messageBody, Resources.Common.Attantion, messageIcon: MessageIcon.Warning);
                    return View(user);
                }
                if (WebDesignComponent.Instance.UserFacade.ChangePassword(user.Id, collection["password"]))
                {
                    ShowMessage(Resources.WebDesign.PasswordSuccedChanged, messageIcon: MessageIcon.Succeed);
                    var role = WebDesignComponent.Instance.UserFacade.Login(user.Username, collection["password"], this.WebSite.Id);
                    if (role != null)
                    {

                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        SessionParameters.WebDesignUser = role;
                        return Redirect("~/WebDesign/UserPanel/Home");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
            }
            return View(user);
        }



        public ActionResult UserForm(Guid formId)

        {
            ViewBag.FormId = formId;
            return View();
        }

        [HttpPost]
        public ActionResult UserForm(FormCollection collection)
        {
            try
            {
                
                var postFormData = this.PostForFormGenerator(collection);
                if (!string.IsNullOrEmpty(postFormData.FillErrors))
                {
                    ShowMessage(postFormData.FillErrors, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Warning);
                    this.ClearFormGeneratorData(postFormData.Id);
                    return View();
                }
                postFormData.RefId = SessionParameters.WebDesignUser.Id.ToString();
                if (FormGeneratorComponent.Instance.FormDataFacade.ModifyFormData(postFormData))
                {
                    ShowMessage(Resources.Common.InsertSuccessMessage, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Succeed);
                    ViewBag.FormId = postFormData.Id;
                    return View();
                }
                ShowMessage(Resources.Common.ErrorInInsert, Resources.Common.MessaageTitle, messageIcon: MessageIcon.Error);
                ViewBag.FormId = postFormData.Id; 
                return View();

            }
            catch (Exception exception)
            {
                ShowExceptionMessage(exception);
                return View();
            }
        }

        public ActionResult GetUserFormInfo(Guid id, string refId, FormState formState = FormState.DataEntryMode)
        {
            var user = FormGeneratorComponent.Instance.WebDesignUserFormsFacade.Get(this.WebSite.Id, id);
            ViewBag.RefId = refId;
            ViewBag.formState = formState;
            return PartialView("PVUserFormInfo", user);
        }
       

    }
}
