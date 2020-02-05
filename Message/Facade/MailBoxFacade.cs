using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//using Radyn.CrossPlatform;
//using Radyn.CrossPlatform.DataStructure;
//using Radyn.CrossPlatform.Tools;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.BO;
using Radyn.Message.DataStructure;
using Radyn.Message.Facade.Interface;
using Radyn.Utility;
//using Enums = Radyn.CrossPlatform.Tools.Enums;

namespace Radyn.Message.Facade
{
    internal sealed class MailBoxFacade : MessageBaseFacade<MailBox>, IMailBoxFacade
    {
        internal MailBoxFacade()
        {
        }

        internal MailBoxFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

       
        public IEnumerable<MailBox> GetInbox(Guid userId)
        {
            try
            {

                var list = new MailBoxBO().GetInbox(this.ConnectionHandler, userId);
                var outlist = new List<MailBox>();
                var mailInfoBo = new MailInfoBO();
                var enterpriseNodeFacade = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade;
                foreach (var item in list)
                {
                    if (outlist.All(box => box.MailId != item.MailId))
                    {
                        var mailInfo = mailInfoBo.Get(base.ConnectionHandler, item.MailId);
                        if (mailInfo != null)
                        {
                            item.MailInfo = mailInfo;
                            item.FromEnterpriseNode = enterpriseNodeFacade.Get(item.FromId);
                        }

                        outlist.Add(item);
                    }
                }
                return outlist.OrderByDescending(x => x.MailInfo.Date);
            }
            catch (KnownException ex)
            {

                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {

             
                throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<MailBox> GetOutbox(Guid userId)
        {
            try
            {
                var list = new MailBoxBO().GetOutInbox(this.ConnectionHandler, userId);
                var outlist = new List<MailBox>();
                var mailInfoBo = new MailInfoBO();

                var enterpriseNodeFacade = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade;
                foreach (var item in list)
                {
                    if (outlist.All(box => box.MailId != item.MailId))
                    {
                        item.MailInfo = mailInfoBo.Get(base.ConnectionHandler, item.MailId);
                        foreach (var toId in item.MailInfo.To.Split(','))
                        {
                            if (!string.IsNullOrEmpty(item.MailInfo.ToNames)) item.MailInfo.ToNames += ",";
                            if (string.IsNullOrEmpty(toId)) continue;
                            var user = enterpriseNodeFacade.Get(toId.ToGuid());
                            if (user != null) item.MailInfo.ToNames += user.Title();
                        }
                        outlist.Add(item);
                    }

                }
                return outlist.OrderByDescending(x => x.MailInfo.Date);
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
        }

        public IEnumerable<MailInfo> GetDraftbox(Guid userId)
        {
            try
            {
                var list = new MailInfoBO().Where(this.ConnectionHandler, x =>
                
                    x.FromId == userId
                );
                var outlist = new List<MailInfo>();
                var enterpriseNodeFacade = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade;
                foreach (var item in list)
                {
                    if (outlist.All(box => box.Id != item.Id))
                    {
                        if (!item.IsDraft) continue;
                        foreach (var toId in item.To.Split(','))
                        {
                            if (!string.IsNullOrEmpty(item.ToNames)) item.ToNames += ",";
                            if (string.IsNullOrEmpty(toId)) continue;
                            var user = enterpriseNodeFacade.Get(toId.ToGuid());
                            if (user != null) item.ToNames += user.Title();
                        }
                        outlist.Add(item);
                    }

                }
                return outlist.OrderByDescending(x => x.Date);
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
        }

        public IEnumerable<MailBox> GetTrash(Guid userId)
        {
            try
            {
                var list = new MailBoxBO().GetUserTrash(this.ConnectionHandler, userId);
                var outlist = new List<MailBox>();
                var mailInfoBo = new MailInfoBO();

                var enterpriseNodeFacade = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade;
                foreach (var item in list)
                {
                    if (outlist.All(box => box.MailId != item.MailId))
                    {
                        item.MailInfo = mailInfoBo.Get(base.ConnectionHandler, item.MailId);
                        
                        foreach (var toId in item.MailInfo.To.Split(','))
                        {
                            if (!string.IsNullOrEmpty(item.MailInfo.ToNames)) item.MailInfo.ToNames += ",";
                            if (string.IsNullOrEmpty(toId)) continue;
                            var user = enterpriseNodeFacade.Get(toId.ToGuid());
                            if (user != null) item.MailInfo.ToNames += user.Title();
                        }
                       
                        outlist.Add(item);
                    }

                }
                return outlist.OrderByDescending(x => x.MailInfo.Date);
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
        }

        public bool SendInternalMail(MailInfo mailInfo, Guid congressId, List<HttpPostedFileBase> file)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                this.FileManagerConnection.StartTransaction(IsolationLevel.ReadCommitted);
                if (!new MailBoxBO().SendInternalMail(this.ConnectionHandler, this.FileManagerConnection, mailInfo, file))
                {
                    throw new Exception("خطایی در ارسال پیام وجود دارد");
                }

                this.ConnectionHandler.CommitTransaction();
                this.FileManagerConnection.CommitTransaction();
                return true;
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
        }
        public bool SendInternalMail(Guid fromId, Guid congressId, string[] toId, string subject, string body, List<Guid> attachment)
        {
            try
            {
               
                if (!new MailBoxBO().SendInternalMail(this.ConnectionHandler, fromId, toId, subject, body, attachment))
                {
                    throw new Exception("خطایی در ارسال پیام وجود دارد");
                }
                return true;
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
        }

        public bool SendInternalMail(Guid fromId, string[] toId, string[] bcc, string subject, string body, List<Guid> attachment)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new MailBoxBO().SendInternalMail(this.ConnectionHandler, fromId, toId, bcc, subject, body, attachment))
                    throw new Exception("خطایی در ارسال پیام وجو دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool SendInternalMail(Guid fromId, string[] toId, string[] bcc, string[] Cc, string subject, string body, List<Guid> attachment)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);

                if (!new MailBoxBO().SendInternalMail(this.ConnectionHandler, fromId, toId, bcc, Cc, subject, body, attachment))
                    throw new Exception("خطایی در ارسال پیام وجو دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

      

        public int GetUnReadInboxCount(Guid userId)
        {
            try
            {
                return new MailBoxBO().GetUnReadInboxCount(this.ConnectionHandler, userId);

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
        }

        public bool HasMail(Guid userId)
        {
            try
            {
                return new MailBoxBO().HasMail(this.ConnectionHandler, userId);

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
        }
        public bool DeleteGroup(string idlist)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new MailBoxBO().DeleteGroup(this.ConnectionHandler, idlist))
                    throw new Exception("خطایی در حذف پیام وجو دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool UnDeleteGroup(string idlist)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new MailBoxBO().UnDeleteGroup(this.ConnectionHandler, idlist))
                    throw new Exception("خطایی در بازگردانی پیام وجو دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public bool DeleteUserMessages(Guid userId)
        {
            try
            {
                this.ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (!new MailBoxBO().DeleteUserMessages(this.ConnectionHandler, userId))
                    throw new Exception("خطایی در ارسال پیام وجو دارد");
                this.ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                this.ConnectionHandler.RollBack();
                Log.Save(ex.Message, LogType.ApplicationError, ex.Source, ex.StackTrace);
                throw new KnownException(ex.Message, ex);
            }
        }

        public IEnumerable<MailBox> GetAllUserMessage(Guid userId)
        {
            try
            {
                var list = new MailBoxBO().Where(this.ConnectionHandler, c=>c.ToId==userId);
               return list;

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
        }


    }
}
