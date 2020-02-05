using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;
using Radyn.Utility;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Radyn.Message.BO
{
    internal class SentMailBO : BusinessBase<SentMail>
    {
        public bool SaveMail(IConnectionHandler connectionHandler, IConnectionHandler filemanagerHandler, SentMail sentmailobj, List<HttpPostedFileBase> Attachments)
        {


            if (!new SentMailBO().Insert(connectionHandler, sentmailobj))
                throw new Exception(Resources.Message.ErrorInSaveMail);
            var fileTransactionalFacade = FileManagerComponent.Instance.FileTransactionalFacade(filemanagerHandler);
            foreach (var httpPostedFileBase in Attachments)
            {

                var mailAttachment = new SentMailAttachment
                {
                    MailId = sentmailobj.Id,
                    FileId = fileTransactionalFacade
                        .Insert(httpPostedFileBase)
                };
                if (!new SentMailAttachmentBO().Insert(connectionHandler, mailAttachment))
                    throw new Exception(Resources.Message.ErrorInSaveEmailAttachment);

            }


            return true;
        }



        public bool SendMail(ref MailMessage mail, string mailHost, string mailPassword, string mailUserName, string mailFrom, short mailPort, string mailSenderDisplayName, bool enableSsl,
                 string To, string subject = "", string Body = "", string Cc = "", string Bcc = "", List<HttpPostedFileBase> Attachments = null)
        {



            if (string.IsNullOrEmpty(mailFrom) || string.IsNullOrEmpty(To)) return false;
            SmtpClient client = new SmtpClient();
            client.Port = mailPort;
            var From = new MailAddress(mailFrom, mailSenderDisplayName);
            mail.From = From;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = mailHost;
            client.EnableSsl = enableSsl;
            var password = @StringUtils.Decrypt(mailPassword);
            client.Credentials = new NetworkCredential(mailFrom, password);
            if (!string.IsNullOrEmpty(Cc))
            {
                string[] cc = Cc.Split(',');
                if (cc.Any())
                {
                    foreach (string item in cc)
                    {
                        if (Utils.IsEmail(item))
                        {
                            var mailaddress = new MailAddress(item, " ");
                            mail.CC.Add(mailaddress);
                        }
                    }
                }
                else
                if (Utils.IsEmail(Cc)) mail.CC.Add(Cc);
            }

            if (!string.IsNullOrEmpty(Bcc))
            {
                string[] bcc = Bcc.Split(',');
                if (bcc.Any())
                {
                    foreach (string item in bcc)
                    {
                        if (Utils.IsEmail(item))
                        {
                            var mailaddress = new MailAddress(item, " ");
                            mail.Bcc.Add(mailaddress);
                        }
                    }
                }
                else
                if (Utils.IsEmail(Bcc)) mail.Bcc.Add(Bcc);
            }
            if (!string.IsNullOrEmpty(To))
            {
                string[] to = To.Split(',');

                if (to.Any())
                {
                    foreach (string item in to)
                    {
                        if (Utils.IsEmail(item))
                        {
                            var mailaddress = new MailAddress(item, " ");
                            mail.To.Add(mailaddress);
                        }
                    }
                }
                else
                    if (Utils.IsEmail(To)) mail.To.Add(To);
            }
            if (Attachments != null)
                foreach (var item in Attachments)
                {
                    var obj = new Attachment(item.InputStream, item.FileName, item.ContentType);
                    mail.Attachments.Add(obj);
                }
            mail.Subject = string.IsNullOrEmpty(subject) ? "No Subject" : subject;
            mail.Body = string.IsNullOrEmpty(Body) ? "http://www.Radyn.ir" : Body;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.Body = mail.Body + "<div style='color:white'> " + DateTime.Now + " </div>";
            client.Send(mail);
            return true;
        }

        public bool SendGroupMail(ref MailMessage mail, string mailHost, string mailPassword, string mailUserName, string mailFrom, short mailPort, string mailSenderDisplayName,
            bool enableSsl, string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null,
            List<HttpPostedFileBase> Attachments = null, bool Persist = false)
        {

            #region SMTP Setting
            if (string.IsNullOrEmpty(mailFrom) || To == null) return false;
            var smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            var From = new MailAddress(mailFrom, mailSenderDisplayName);
            mail.From = From;
            //mail.Sender = From;
            smtpClient.Host = mailHost;
            smtpClient.Port = mailPort;
            smtpClient.EnableSsl = enableSsl;
            var password = StringUtils.Decrypt(mailPassword);
            smtpClient.Credentials = new NetworkCredential(mailFrom, password);
            #endregion

            #region CC

            if (Cc != null && Cc.Any())
            {
                foreach (string item in Cc)
                {
                    if (Utils.IsEmail(item))
                    {
                        var mailaddress = new MailAddress(item, " ");
                        mail.CC.Add(mailaddress);
                    }
                }
            }

            #endregion

            #region Bcc


            if (Bcc != null && Bcc.Any())
            {
                foreach (string item in Bcc)
                {
                    if (Utils.IsEmail(item))
                    {
                        var mailaddress = new MailAddress(item, " ");
                        mail.Bcc.Add(mailaddress);
                    }
                }
            }


            #endregion

            #region TO

            if (To.Any())
            {
                foreach (string item in To)
                {
                    if (Utils.IsEmail(item))
                    {
                        var mailaddress = new MailAddress(item, " ");
                        if (To.Count() == 1) mail.To.Add(mailaddress);
                        else mail.Bcc.Add(mailaddress);
                    }
                }
            }

            #endregion

            #region Attachment
            if (Attachments != null)
                foreach (var item in Attachments)
                {
                    var obj = new Attachment(item.InputStream, item.FileName, item.ContentType);
                    mail.Attachments.Add(obj);
                }
            #endregion

            //Subject
            mail.Subject = string.IsNullOrEmpty(subject) ? "No Subject" : subject;

            //Body
            mail.Body = string.IsNullOrEmpty(Body) ? "http://www.Radyn.ir" : Body;

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;
            mail.BodyEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            smtpClient.Send(mail);
            return true;
        }

        public bool SendGroupMailWithInterval(ref MailMessage mail, string mailHost, string mailPassword, string mailUserName, string mailFrom,
            short mailPort, string mailSenderDisplayName, bool enableSsl, string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null,
            List<HttpPostedFileBase> Attachments = null, bool Persist = false, int intervalSecond = 20)
        {
            #region SMTP Setting

            if (string.IsNullOrEmpty(mailFrom) || To == null) return false;

            var smtpClient = new SmtpClient();
            //var From = new MailAddress(mailFrom, mailSenderDisplayName);
            MailAddress From;
            if (mailHost == "smtp.gmail.com")
            {
                From = new MailAddress(mailFrom, "", Encoding.UTF8);
            }
            else
            {
                From = new MailAddress(mailFrom, mailSenderDisplayName, Encoding.UTF8);
            }

            mail.From = From;
            //mail.Sender = From;
            smtpClient.Host = mailHost;
            smtpClient.Port = mailPort;
            smtpClient.EnableSsl = enableSsl;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            var password = StringUtils.Decrypt(mailPassword);
            smtpClient.Credentials = new NetworkCredential(mailFrom, password);
            #endregion

            var sendList = new List<string>();
            if (To.Any())
                sendList.AddRange(To.Where(Utils.IsEmail));
            if (Bcc != null)
                sendList.AddRange(Bcc.Where(Utils.IsEmail));
            if (Cc != null)
                sendList.AddRange(Cc.Where(Utils.IsEmail));
            #region Attachment
            if (Attachments != null)
                foreach (var item in Attachments)
                {
                    var obj = new Attachment(item.InputStream, item.FileName, item.ContentType);
                    mail.Attachments.Add(obj);
                }
            #endregion

            //Subject
            mail.Subject = string.IsNullOrEmpty(subject) ? "No Subject" : subject;

            //Body
            mail.Body = string.IsNullOrEmpty(Body) ? "http://www.Radyn.ir" : Body;

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.HeadersEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            var intervalSendMail = new IntervalSendMail
            {
                Interval = intervalSecond,
                List = sendList,
                MailMessage = mail,
                SmtpClient = smtpClient
            };
            Task.Factory.StartNew(() => SendMailTask(intervalSendMail));
            return true;
        }

        private void SendMailTask(IntervalSendMail obj)
        {


            foreach (var to in obj.List)
            {
                try
                {
                    obj.MailMessage.To.Clear();
                    obj.MailMessage.To.Add(to);
                    if (obj.SmtpClient.EnableSsl)
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                       delegate (object s, X509Certificate certificate,
                                 X509Chain chain, SslPolicyErrors sslPolicyErrors)
                       { return true; };
                    }

                    obj.SmtpClient.Send(obj.MailMessage);
                    System.Threading.Thread.Sleep(obj.Interval * 1000);
                }
                catch (Exception ex)
                {

                }
            }
        }

        internal sealed class IntervalSendMail
        {

            public SmtpClient SmtpClient { get; set; }
            public MailMessage MailMessage { get; set; }
            public int Interval { get; set; }
            public List<string> List { get; set; }
        }
    }
}
