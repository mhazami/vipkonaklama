using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.BO;
using Radyn.Message.DataStructure;
using Radyn.Message.Facade.Interface;
using Radyn.Message.Tools;
using MailMessage = System.Net.Mail.MailMessage;

namespace Radyn.Message.Facade
{
    internal sealed class SentMailFacade : MessageBaseFacade<SentMail>, ISentMailFacade
    {
        internal SentMailFacade()
        {
        }

        internal SentMailFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

        private static MailSetting SetSmtpSection()
        {
            var smtpSection = (MailSetting) ConfigurationManager.GetSection("radyn/smtp");
            return smtpSection;
        }

        private MailMessage mail = new MailMessage();

        public bool SendMail(string To, string subject = "", string Body = "", string Cc = "", string Bcc = "",
            List<HttpPostedFileBase> Attachments = null,
            bool Persist = false)
        {
            bool result;
            var cnfSmtp = SetSmtpSection();
            try
            {
                if (
                    !new SentMailBO().SendMail(ref mail, cnfSmtp.MailHost, cnfSmtp.MailPassword, cnfSmtp.MailUserName,
                        cnfSmtp.MailFrom, cnfSmtp.MailPort, cnfSmtp.MailSenderDisplayName, cnfSmtp.EnableSSL, To,
                        subject, Body, Cc, Bcc, Attachments))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                result = true;
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            try
            {
                if (!Persist) return result;
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (Persist)
                {
                    var sentmailobj = new SentMail
                    {
                        SMTPserver = cnfSmtp.MailHost,
                        To = String.Join(",", mail.To),
                        CC = String.Join(",", mail.CC),
                        Bcc = String.Join(",", mail.Bcc),
                        Body = Body,
                        Subject = subject,
                        From = cnfSmtp.MailFrom
                    };
                    sentmailobj.SMTPserver = cnfSmtp.MailHost;
                    sentmailobj.Priority = "Normal";
                    sentmailobj.SendDate = DateTime.Now;
                    if (
                        !new SentMailBO().SaveMail(this.ConnectionHandler, this.FileManagerConnection, sentmailobj,
                            Attachments))
                        throw new Exception(Resources.Message.ErrorInSaveMail);

                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            return result;
        }

        public bool SendGroupMail(string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null,
            List<HttpPostedFileBase> Attachments = null, bool Persist = false)
        {
            bool result;
            var cnfSmtp = SetSmtpSection();
            try
            {
                if (
                    !new SentMailBO().SendGroupMail(ref mail, cnfSmtp.MailHost, cnfSmtp.MailPassword,
                        cnfSmtp.MailUserName, cnfSmtp.MailFrom, cnfSmtp.MailPort, cnfSmtp.MailSenderDisplayName,
                        cnfSmtp.EnableSSL, To, subject, Body, Cc, Bcc, Attachments))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                result = true;
            }
            catch 
            {

                throw new KnownException("مشکلی در ارسال ایمیل وجود دارد");
            }
            try
            {
                if (!Persist) return result;
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (Persist)
                {

                    var sentmailobj = new SentMail
                    {
                        SMTPserver = cnfSmtp.MailHost,
                        To = String.Join(",", mail.To),
                        CC = String.Join(",", mail.CC),
                        Bcc = String.Join(",", mail.Bcc),
                        Body = Body,
                        Subject = subject,
                        From = cnfSmtp.MailFrom
                    };
                    sentmailobj.SMTPserver = cnfSmtp.MailHost;
                    sentmailobj.Priority = "Normal";
                    sentmailobj.SendDate = DateTime.Now;

                    if (
                        !new SentMailBO().SaveMail(this.ConnectionHandler, this.FileManagerConnection, sentmailobj,
                            Attachments))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            return result;
        }

        public bool SendGroupMailWithInterval(string[] To, string subject, string Body, string[] Cc = null,
            string[] Bcc = null,
            List<HttpPostedFileBase> Attachments = null, bool Persist = false, int intervalSecond = 20)
        {
            bool result;
            var cnfSmtp = SetSmtpSection();
            try
            {
                if (
                    !new SentMailBO().SendGroupMailWithInterval(ref mail, cnfSmtp.MailHost, cnfSmtp.MailPassword,
                        cnfSmtp.MailUserName, cnfSmtp.MailFrom, cnfSmtp.MailPort, cnfSmtp.MailSenderDisplayName,
                        cnfSmtp.EnableSSL, To, subject, Body, Cc, Bcc, Attachments))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                result = true;
            }
            catch 
            {

                throw new KnownException("مشکلی در ارسال ایمیل وجود دارد");
            }
            
            try
            {
                if (!Persist) return result;
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (Persist)
                {

                    var sentmailobj = new SentMail
                    {
                        SMTPserver = cnfSmtp.MailHost,
                        To = String.Join(",", mail.To),
                        CC = String.Join(",", mail.CC),
                        Bcc = String.Join(",", mail.Bcc),
                        Body = Body,
                        Subject = subject,
                        From = cnfSmtp.MailFrom
                    };
                    sentmailobj.SMTPserver = cnfSmtp.MailHost;
                    sentmailobj.Priority = "Normal";
                    sentmailobj.SendDate = DateTime.Now;

                    if (
                        !new SentMailBO().SaveMail(this.ConnectionHandler, this.FileManagerConnection, sentmailobj,
                            Attachments))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            return result;
        }

        public bool SendMail(string mailHost, string mailPassword, string mailUserName, string mailFrom, short mailPort,

            string mailSenderDisplayName, bool enableSsl,
            string To, string subject = "", string Body = "", string Cc = "", string Bcc = "",
            List<HttpPostedFileBase> Attachments = null,
            bool Persist = false)
        {
          
            bool result;
            try
            {
                if (
                    !new SentMailBO().SendMail(ref mail, mailHost, mailPassword, mailUserName, mailFrom, mailPort,
                        mailSenderDisplayName, enableSsl, To, subject, Body, Cc, Bcc, Attachments))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                result = true;
            }
           
            catch(Exception ex)
            {

                throw new KnownException("مشکلی در ارسال ایمیل وجود دارد");
            }
            try
            {
                if (!Persist) return result;
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (Persist)
                {

                    var sentmailobj = new SentMail
                    {
                        SMTPserver = mailHost,
                        To = String.Join(",", mail.To),
                        CC = String.Join(",", mail.CC),
                        Bcc = String.Join(",", mail.Bcc),
                        Body = Body,
                        Subject = subject,
                        From = mailFrom
                    };
                    sentmailobj.SMTPserver = mailHost;
                    sentmailobj.Priority = "Normal";
                    sentmailobj.SendDate = DateTime.Now;

                    if (
                        !new SentMailBO().SaveMail(this.ConnectionHandler, this.FileManagerConnection, sentmailobj,
                            Attachments))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            return result;
        }

        public bool SendGroupMail(string mailHost, string mailPassword, string mailUserName, string mailFrom,
            short mailPort, string mailSenderDisplayName,
            bool enableSsl, string[] To, string subject, string Body, string[] Cc = null, string[] Bcc = null,
            List<HttpPostedFileBase> Attachments = null, bool Persist = false)
        {
            bool result;
            try
            {
                if (
                    !new SentMailBO().SendGroupMail(ref mail, mailHost, mailPassword, mailUserName, mailFrom, mailPort,
                        mailSenderDisplayName, enableSsl, To, subject, Body, Cc, Bcc, Attachments))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                result = true;
            }
            catch
            {

                throw new KnownException("مشکلی در ارسال ایمیل وجود دارد");
            }
            
            try
            {
                if (!Persist) return result;
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (Persist)
                {

                    var sentmailobj = new SentMail
                    {
                        SMTPserver = mailHost,
                        To = String.Join(",", mail.To),
                        CC = String.Join(",", mail.CC),
                        Bcc = String.Join(",", mail.Bcc),
                        Body = Body,
                        Subject = subject,
                        From = mailFrom
                    };
                    sentmailobj.SMTPserver = mailHost;
                    sentmailobj.Priority = "Normal";
                    sentmailobj.SendDate = DateTime.Now;

                    if (
                        !new SentMailBO().SaveMail(this.ConnectionHandler, this.FileManagerConnection, sentmailobj,
                            Attachments))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            return result;
        }

        public bool SendGroupMailWithInterval(string mailHost, string mailPassword, string mailUserName, string mailFrom,
            short mailPort, string mailSenderDisplayName, bool enableSsl, string[] To, string subject, string Body,
            string[] Cc = null, string[] Bcc = null,
            List<HttpPostedFileBase> Attachments = null, bool Persist = false, int intervalSecond = 20)
        {
            bool result;
            try
            {
                if (
                    !new SentMailBO().SendGroupMailWithInterval(ref mail, mailHost, mailPassword, mailUserName, mailFrom,
                        mailPort, mailSenderDisplayName, enableSsl, To, subject, Body, Cc, Bcc, Attachments))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                result = true;
            }
            catch
            {

                throw new KnownException("مشکلی در ارسال ایمیل وجود دارد");
            }
            try
            {
                if (!Persist) return result;
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (Persist)
                {

                    var sentmailobj = new SentMail
                    {
                        SMTPserver = mailHost,
                        To = String.Join(",", mail.To),
                        CC = String.Join(",", mail.CC),
                        Bcc = String.Join(",", mail.Bcc),
                        Body = Body,
                        Subject = subject,
                        From = mailFrom
                    };
                    sentmailobj.SMTPserver = mailHost;
                    sentmailobj.Priority = "Normal";
                    sentmailobj.SendDate = DateTime.Now;

                    if (
                        !new SentMailBO().SaveMail(this.ConnectionHandler, this.FileManagerConnection, sentmailobj,
                            Attachments))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                this.FileManagerConnection.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            return result;
        }



    }
}
