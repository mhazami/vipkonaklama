using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Radyn.FileManager;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;
using Radyn.Utility;

namespace Radyn.Message.BO
{
    internal class MailBoxBO : BusinessBase<MailBox>
    {

        public override bool Insert(IConnectionHandler connectionHandler, MailBox obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);

        }
        public bool SendInternalMail(IConnectionHandler connectionHandler, IConnectionHandler fileManagerConnection, MailInfo mailInfo, List<HttpPostedFileBase> file)
        {
            var fileTransactionalFacade =
                FileManagerComponent.Instance.FileTransactionalFacade(fileManagerConnection);
            var list = new List<Guid>();
            if (file != null)
                list.AddRange(file.Select(fileTransactionalFacade.Insert));
            if (!SentMailInBo(connectionHandler, mailInfo, list))
                throw new Exception("خطایی در ارسال پیام وجود دارد");
            return true;
        }
        public bool SendInternalMail(IConnectionHandler connectionHandler, Guid fromId, string[] toId, string subject, string body, List<Guid> attachment)
        {
            var mailInfo = new MailInfo { To = string.Join(",", toId), FromId = fromId, Subject = subject, Body = body };
            if (!SentMailInBo(connectionHandler, mailInfo, attachment))
                throw new Exception("خطایی در ارسال ایمیل وجود دارد");
            return true;
        }
        public bool SendInternalMail(IConnectionHandler connectionHandler, Guid fromId, string[] toId, string[] bcc, string subject, string body, List<Guid> attachment)
        {
            var mailInfo = new MailInfo { To = string.Join(",", toId), Bcc = string.Join(",", bcc), FromId = fromId, Subject = subject, Body = body };
            if (!SentMailInBo(connectionHandler, mailInfo, attachment))
                throw new Exception("خطایی در ارسال ایمیل وجود دارد");
            return true;
        }
        public bool SendInternalMail(IConnectionHandler connectionHandler, Guid fromId, string[] toId, string[] bcc, string[] Cc, string subject, string body, List<Guid> attachment)
        {
            var mailInfo = new MailInfo { To = string.Join(",", toId), Bcc = string.Join(",", bcc), Cc = string.Join(",", Cc), FromId = fromId, Subject = subject, Body = body };
            if (!SentMailInBo(connectionHandler, mailInfo, attachment))
                throw new Exception("خطایی در ارسال ایمیل وجو دارد");
            return true;
        }
        public bool SentMailInBo(IConnectionHandler connectionHandler, MailInfo mailInfo, List<Guid> file)
        {

            if (mailInfo.Id == Guid.Empty)
            {
                if (!new MailInfoBO().Insert(connectionHandler, mailInfo))
                {
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                }
            }
            else
            {
                if (!new MailInfoBO().Update(connectionHandler, mailInfo))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
            }

            if (file != null)
            {
                if (file.Select(attachId => new MailAttachment
                {
                    MailId = mailInfo.Id,
                    AttachId = attachId
                }).Any(mailAttachment => !new MailAttachmentBO().Insert(connectionHandler, mailAttachment)))
                    throw new Exception(Resources.Message.ErrorInSaveEmailAttachment);
            }
            if (!mailInfo.IsDraft)
            {
                var mailBoxBo = new MailBoxBO();
                if (string.IsNullOrEmpty(mailInfo.To))
                    throw new Exception("لطفا اشخاص ارسالی را انتخاب کنید");
                if (mailInfo.To.Split(',').Select(toId => new MailBox
                {
                    MailId = mailInfo.Id,
                    ToId = toId.ToGuid(),
                    FromId = mailInfo.FromId,
                }).Any(mailBox => !mailBoxBo.Insert(connectionHandler, mailBox)))
                    throw new Exception(Resources.Message.ErrorInSaveMail);
                if (!string.IsNullOrEmpty(mailInfo.Bcc))
                {
                    if (mailInfo.Bcc.Split(',').Select(toId => new MailBox
                    {
                        MailId = mailInfo.Id,
                        ToId = toId.ToGuid(),
                        FromId = mailInfo.FromId,
                    }).Any(mailBox => !mailBoxBo.Insert(connectionHandler, mailBox)))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
                if (!string.IsNullOrEmpty(mailInfo.Cc))
                {
                    if (mailInfo.Cc.Split(',').Select(toId => new MailBox
                    {
                        MailId = mailInfo.Id,
                        ToId = toId.ToGuid(),
                        FromId = mailInfo.FromId,
                    }).Any(mailBox => !mailBoxBo.Insert(connectionHandler, mailBox)))
                        throw new Exception(Resources.Message.ErrorInSaveMail);
                }
            }

            return true;
        }
        public List<MailBox> GetUserInboxMail(IConnectionHandler connectionHandler, Guid userId, Guid id)
        {

            var query = new PredicateBuilder<MailBox>();
            if (userId != Guid.Empty)
                query.And(c => c.ToId == userId);
            if (id != Guid.Empty)
                query.And(c => c.MailId == id);
            var res = this.Where(connectionHandler, query.GetExpression());
            return res;
        }
        public MailBox GetUserOutInboxMail(IConnectionHandler connectionHandler, Guid userId, Guid id)
        {
            
            var res = this.FirstOrDefault(connectionHandler, c => c.FromId == userId&& c.MailId == id);
            return res;
        }
        public int GetUnReadInboxCount(IConnectionHandler connectionHandler, Guid userId)
        {
           
           return this.Count(connectionHandler, c => c.Id, x=>x.ToId == userId&&x.Read==false);
            

        }
        public bool HasMail(IConnectionHandler connectionHandler, Guid userId)
        {
            
            return this.Any(connectionHandler,  c=>  c.ToId == userId || c.FromId == userId);
           
        }
        public bool DeleteUserMessages(IConnectionHandler connectionHandler, Guid userId)
        {
           

            var res = this.Where(connectionHandler, c => c.ToId == userId || c.FromId == userId);
            foreach (var mailBox in res)
            {
                base.Delete(connectionHandler, mailBox);
            }

            return true;
        }
        public IEnumerable<MailBox> GetAllUserMessage(IConnectionHandler connectionHandler, Guid userId)
        {
            
            var res = OrderByDescending(connectionHandler, c => c.Read, c => c.ToId == userId || c.FromId == userId);
            return res;
        }
        public IEnumerable<MailBox> GetUserTrash(IConnectionHandler connectionHandler, Guid userId)
        {
            


            var res = this.OrderByDescending(connectionHandler, c => c.Read, c => (c.ToId == userId || c.FromId == userId) && c.Deleted);
            return res;

        }
        public bool DeleteGroup(IConnectionHandler connectionHandler, string idlist)
        {
            if (string.IsNullOrEmpty(idlist)) return false;
            return DeleteChild(connectionHandler,
                 (from s in idlist.Split(',') where !string.IsNullOrEmpty(s) select s.ToGuid()).ToList());

        }
        private bool DeleteChild(IConnectionHandler connectionHandler, IEnumerable<Guid> idlist, bool delete = true)
        {
            foreach (var variable in idlist)
            {
                var item = this.Get(connectionHandler, variable);
                if (item == null) continue;
                item.Deleted = delete;
                if (!this.Update(connectionHandler, item))
                    throw new Exception("خطایی در حذف وجود دارد");
                var enumerable =
                    this.Where(connectionHandler, x =>

                        x.MailId == item.MailId &&
                        x.FromId == item.FromId &&
                        x.Id != item.Id
                    );
                foreach (var mailBox in enumerable)
                {
                    mailBox.Deleted = delete;
                    if (!this.Update(connectionHandler, mailBox))
                        throw new Exception("خطایی در حذف وجود دارد");
                }


            }
            return true;
        }
        public bool UnDeleteGroup(IConnectionHandler connectionHandler, string idlist)
        {
            if (string.IsNullOrEmpty(idlist)) return false;
            return DeleteChild(connectionHandler,
               (from s in idlist.Split(',') where !string.IsNullOrEmpty(s) select s.ToGuid()).ToList(), false);
        }
        public IEnumerable<MailBox> GetInbox(IConnectionHandler connectionHandler, Guid userId)
        {
           
            var res = this.OrderByDescending(connectionHandler, c => c.Read, c => c.ToId == userId && c.Deleted == false);
            return res;
        }
        public IEnumerable<MailBox> GetOutInbox(IConnectionHandler connectionHandler, Guid userId)
        {
            

            var res = this.OrderByDescending(connectionHandler, c => c.Read, c => c.FromId == userId && c.Deleted == false);
            return res;
        }
    }
}
