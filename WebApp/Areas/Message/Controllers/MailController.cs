using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Radyn.Message;
using Radyn.Message.DataStructure;
using Radyn.WebApp.AppCode.Base;

namespace Radyn.WebApp.Areas.Message.Controllers
{
    public class MailController : WebDesignBaseController
    {
        //
        // GET: /Mail/Mail/

        public ActionResult Index()
        {
            return View(new SentMail());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Index(FormCollection collection)
        {

            var obj = new SentMail();
            this.RadynTryUpdateModel(obj, collection);
            if (!string.IsNullOrEmpty(obj.To))
            {
                if (collection["Usetemplate"].Contains("true"))
                {

                    //var Emailtemplate =
                    //    XmlReaderComponent.XmlFile[AppCode.Constants.XmlReader.StaticPages, "RadynMailTemplate"];
                    //var tmp = Emailtemplate.Replace("<MessageSubject>", obj.Subject);
                    //tmp = tmp.Replace("<MessageBody>", obj.Body);
                    //obj.Body = tmp;
                }
                obj.SendDate = DateTime.Now;

                var fileAtachments = new List<HttpPostedFileBase>();
                for (int i = 0; i < 10; i++)
                {
                    string index = "File" + i;
                    var fileAttach = (HttpPostedFileBase)RadynSession[index];
                    if (fileAttach != null)
                    {
                        RadynSession.Remove(index);
                        fileAtachments.Add(fileAttach);
                    }
                    else
                        break;
                }

                
                MessageComponenet.Instance.MailFacade.SendMail(obj.To, obj.Subject, obj.Body, Attachments: fileAtachments,
                              Bcc: obj.Bcc, Cc: obj.CC, Persist: true);
                return Content("Email Sent");
            }
            return null;
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Send(FormCollection collection)
        {

            var obj = new SentMail();
            this.RadynTryUpdateModel(obj, collection);
            if (!string.IsNullOrEmpty(obj.To))
            {
                var Template = collection["Usetemplate"];
                if (Template != null)
                {
                    if (Template.Contains("true"))
                    {

                        //var Emailtemplate =
                        //    XmlReaderComponent.XmlFile[AppCode.Constants.XmlReader.StaticPages, "RadynMailTemplate"];
                        //var tmp = Emailtemplate.Replace("<MessageSubject>", obj.Subject);
                        //tmp = tmp.Replace("<MessageBody>", obj.Body);
                        //obj.Body = tmp;
                    }
                }


                obj.SendDate = DateTime.Now;

                var fileAtachments = new List<HttpPostedFileBase>();
                for (int i = 0; i < 10; i++)
                {
                    string index = "File" + i;
                    var fileAttach = (HttpPostedFileBase)RadynSession[index];
                    if (fileAttach == null)
                        break;
                    RadynSession.Remove(index);
                    fileAtachments.Add(fileAttach);
                }

                
                MessageComponenet.Instance.MailFacade.SendMail(obj.To, obj.Subject, obj.Body, Attachments: fileAtachments,
                              Bcc: obj.Bcc, Cc: obj.CC, Persist: true);
                return Content(Resources.Message.SentMailMessage);
            }
            return null;
        }
        public ActionResult Uploadfile(IEnumerable<HttpPostedFileBase> fileBase)
        {
            HttpPostedFileBase file = Request.Files["upFile"];
            if (file != null)
            {
                if (file.InputStream != null)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        string index = "File" + i;
                        if (RadynSession[index] == null)
                        {
                            RadynSession[index] = file;
                            break;
                        }
                    }
                }
            }
            return Content("");
        }

        public ActionResult Send(string To = "", string Subject = "", string Body = "", string CC = "", string BCC = "", bool persist = true, string Date = "", string CId = "")
        {
            var mail = new SentMail { To = To, Subject = Subject, Body = Body, CC = CC, Bcc = BCC, SMTPserver = CId };


            //User As Temp for hold CommentId

            return PartialView("MailSender", mail);
        }
    }
}
