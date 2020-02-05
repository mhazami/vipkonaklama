using System;
using System.Collections.Generic;
using System.Web;
using Radyn.Framework;
using Radyn.Message.DataStructure;

namespace Radyn.Message.Facade.Interface
{
    public interface IMailBoxFacade : IBaseFacade<MailBox>
    {
        IEnumerable<MailBox> GetInbox(Guid userId);
        IEnumerable<MailBox> GetOutbox(Guid userId);
        IEnumerable<MailInfo> GetDraftbox(Guid userId);
        IEnumerable<MailBox> GetTrash(Guid userId);

        bool SendInternalMail(MailInfo mailInfo, Guid congressId, List<HttpPostedFileBase> file);
        bool SendInternalMail(Guid fromId, Guid congressId, string[] toId,string title,string body,List<Guid> attachment=null);
        bool SendInternalMail(Guid fromId, string[] toId, string[] bcc, string title, string body, List<Guid> attachment = null);
        bool SendInternalMail(Guid fromId, string[] toId, string[] bcc, string[] css, string title, string body, List<Guid> attachment = null);
        int GetUnReadInboxCount(Guid userId);
        bool HasMail(Guid userId);
        bool DeleteUserMessages(Guid userId);
        IEnumerable<MailBox> GetAllUserMessage(Guid userId);
        bool DeleteGroup(string id);
        bool UnDeleteGroup(string id);
    }
}
