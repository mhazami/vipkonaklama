using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Radyn.Message;
using Radyn.Message.DataStructure;
using Radyn.Message.Tools;
using Radyn.Web.Mvc.UI.Message;
using Radyn.WebApp.AppCode.Base;
using Radyn.WebApp.AppCode.Security;

namespace Radyn.WebApp.Areas.Message.Controllers
{
    public class InternalMailController : WebDesignBaseController
    {

        public ActionResult UserIndex(Guid userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        public ActionResult Index(Guid userId)
        {
            ViewBag.UserId = userId;
           return View();
        }
        public ActionResult GetIndex(Guid userId)
        {
            ViewBag.UserId = userId; 
            ViewBag.ReadCount = MessageComponenet.SentInternalMessageInstance.MailBoxFacade.GetUnReadInboxCount(userId);
            return PartialView("PVGetIndex");
        }

        public ActionResult GetSectionCount(Guid userId)
        {
            try
            {
                var count =
                           MessageComponenet.SentInternalMessageInstance.MailBoxFacade.GetUnReadInboxCount(userId);
                return Json(new { Value = count.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json(new {Value = ""}, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMailPartial(Enums.MailSection value, Guid userId)
        {
            ViewBag.UserId = userId;
            switch (value)
            {
                case Enums.MailSection.Compose:
                    {
                        return PartialView("PVCompose", new MailInfo() { IsDraft = false, FromId = userId });
                    }
                case Enums.MailSection.Inbox:
                    {
                        var list =
                            MessageComponenet.SentInternalMessageInstance.MailBoxFacade.GetInbox(userId);
                        return PartialView("PVMailInbox", list);
                    }
                case Enums.MailSection.OutBox:
                    {
                        var list =
                           MessageComponenet.SentInternalMessageInstance.MailBoxFacade.GetOutbox(userId);
                        return PartialView("PVMailOutBox", list);
                    }
                case Enums.MailSection.Draft:
                    {
                        var list =
                           MessageComponenet.SentInternalMessageInstance.MailBoxFacade.GetDraftbox(userId);
                        return PartialView("PVMailDraft", list);
                    }
                case Enums.MailSection.Trash:
                    {
                        var list =
                           MessageComponenet.SentInternalMessageInstance.MailBoxFacade.GetTrash(userId);
                        return PartialView("PVMailTrash", list);
                    }

            }
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ComposeMail(FormCollection collection)
        {
            try
            {
                var mailInfo = new MailInfo();
                this.RadynTryUpdateModel(mailInfo, collection);
                List<HttpPostedFileBase> file = null;
                if (RadynSession["MailAttachment"] != null)
                {
                    file = RadynSession["MailAttachment"] as List<HttpPostedFileBase>;
                    RadynSession.Remove("MailAttachment");
                }
                return
                    Content(MessageComponenet.SentInternalMessageInstance.MailBoxFacade.SendInternalMail(mailInfo, SessionParameters.CurrentWebSite.Id, file)
                        ? "true"
                        : "false");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                ViewBag.Message = ex.Message;
                return Content("false");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveMail(FormCollection collection)
        {
            try
            {
                var mailInfo = new MailInfo();
                this.RadynTryUpdateModel(mailInfo, collection);
                mailInfo.IsDraft = true;
                mailInfo.Id = Guid.Empty;
                List<HttpPostedFileBase> file = null;
                if (RadynSession["MailAttachment"] != null)
                {
                    file = RadynSession["MailAttachment"] as List<HttpPostedFileBase>;
                    RadynSession.Remove("MailAttachment");
                }
                return
                    Content(MessageComponenet.SentInternalMessageInstance.MailBoxFacade.SendInternalMail(mailInfo, SessionParameters.CurrentWebSite.Id, file)
                        ? "true"
                        : "false");
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                ViewBag.Message = ex.Message;
                return Content("false");
            }
        }

        public ActionResult GetModify(Guid userId, Guid? Id)
        {
            var mailInfo = Id.HasValue ? MessageComponenet.SentInternalMessageInstance.MailInfoFacade.GetMail(userId, (Guid)Id) : new MailInfo() { FromId = userId };
            return PartialView("PVModify", mailInfo);
        }
        public ActionResult ViewEmail(Guid userId, Guid Id,bool allowreply=false)
        {
            var mailInfo = MessageComponenet.SentInternalMessageInstance.MailInfoFacade.GetMail(userId, Id);
            ViewBag.FromId = userId;
            ViewBag.allowreply = allowreply;
            return PartialView("PVViewEmail", mailInfo);
        }
        public ActionResult UnDelete(string Id)
        {
            try
            {
                return
                   Content(MessageComponenet.SentInternalMessageInstance.MailBoxFacade.UnDeleteGroup(Id)
                       ? "true"
                       : "false");
            }
           
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
                return Content("false");
            }
        }
        public ActionResult DeleteDraft(string Id)
        {
            try
            {
                return
                   Content(MessageComponenet.SentInternalMessageInstance.MailInfoFacade.DeleteGroup(Id)
                       ? "true"
                       : "false");
            }

            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
                return Content("false");
            }
        }
        public ActionResult GroupDelete(string Id)
        {
            try
            {
                return
                   Content(MessageComponenet.SentInternalMessageInstance.MailBoxFacade.DeleteGroup(Id)
                       ? "true"
                       : "false");
            }

            catch (Exception ex)
            {
                ShowMessage(ex.Message, "", messageIcon: MessageIcon.Security);
                ViewBag.Message = ex.Message;
                return Content("false");
            }
        }
        
        public ActionResult LookUpSelectUser(Guid userId, string value)
        {
            ViewBag.Values = value;
            ViewBag.UserId = userId;
            return View();
        }
        public ActionResult GetUsers(Guid userId, string selected, string searchvalue)
        {
            try
            {
               
                var searchUsers = MessageComponenet.SentInternalMessageInstance.MailInfoFacade.SearchUsers(userId, selected, searchvalue);
                ViewBag.ShowNoresulyFound = !string.IsNullOrEmpty(selected)||!string.IsNullOrEmpty(searchvalue);
                return PartialView("PVSearchResult", searchUsers);
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return Content("false");
            }
        }

    }
}
