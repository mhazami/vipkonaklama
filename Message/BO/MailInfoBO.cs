using System;
using System.Collections.Generic;
using System.Linq;
using Radyn.EnterpriseNode;
using Radyn.EnterpriseNode.Tools;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;
using Radyn.Message.Tools;
using Radyn.Utility;

namespace Radyn.Message.BO
{
    internal class MailInfoBO : BusinessBase<MailInfo>
    {

        public override bool Insert(IConnectionHandler connectionHandler, MailInfo obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            obj.Date = DateTime.Now;
            return base.Insert(connectionHandler, obj);
        }

        public override bool Update(IConnectionHandler connectionHandler, MailInfo obj)
        {
            if (obj.Date == DateTime.MinValue)
                obj.Date = DateTime.Now;
            return base.Update(connectionHandler, obj);
        }
        public MailInfo GetMail(IConnectionHandler connectionHandler, Guid userId, Guid id)
        {

            var mailInfo = this.Get(connectionHandler, id);
            if (mailInfo == null) return null;
            var inboxbox = new MailBoxBO().GetUserInboxMail(connectionHandler, userId, id);
            var enterpriseNodeFacade = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade;
            if (inboxbox != null)
            {
                foreach (var mailBox in inboxbox)
                {
                    if (!mailBox.Read)
                    {
                        mailBox.Read = true;
                        if (!new MailBoxBO().Update(connectionHandler, mailBox))
                            throw new Exception("خطایی در ویرایش ایمیل وجود دارد");
                    }
                }

                if (!string.IsNullOrEmpty(mailInfo.Bcc))
                {
                    foreach (var bccId in mailInfo.Bcc.Split(','))
                    {
                        if (string.IsNullOrEmpty(bccId)) continue;
                        if (!string.IsNullOrEmpty(mailInfo.BccNames)) mailInfo.BccNames += ",";
                        if (userId == bccId.ToGuid())
                        {
                            var user = enterpriseNodeFacade.Get(bccId.ToGuid());
                            if (user != null) mailInfo.BccNames += user.Title();
                        }
                    }
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(mailInfo.Bcc))
                {
                    foreach (var bccId in mailInfo.Bcc.Split(','))
                    {
                        if (string.IsNullOrEmpty(bccId)) continue;
                        if (!string.IsNullOrEmpty(mailInfo.BccNames)) mailInfo.BccNames += ",";
                        var user = enterpriseNodeFacade.Get(bccId.ToGuid());
                        if (user != null) mailInfo.BccNames += user.Title();
                    }
                }
            }
            if (!string.IsNullOrEmpty(mailInfo.To))
            {
                foreach (var toId in mailInfo.To.Split(','))
                {
                    if (string.IsNullOrEmpty(toId)) continue;
                    if (!string.IsNullOrEmpty(mailInfo.ToNames)) mailInfo.ToNames += ",";
                    var user = enterpriseNodeFacade.Get(toId.ToGuid());
                    if (user != null) mailInfo.ToNames += user.Title();
                }
            }
            if (!string.IsNullOrEmpty(mailInfo.Cc))
            {
                foreach (var ccId in mailInfo.Cc.Split(','))
                {
                    if (string.IsNullOrEmpty(ccId)) continue;
                    if (!string.IsNullOrEmpty(mailInfo.CcNames)) mailInfo.CcNames += ",";
                    var user = enterpriseNodeFacade.Get(ccId.ToGuid());
                    if (user != null) mailInfo.CcNames += user.Title();
                }
            }
            var list = new MailAttachmentBO().Where(connectionHandler, x => x.MailId == id);
            foreach (var mailAttachment in list)
            {
                
                if (mailAttachment.AttachFile == null) continue;
                var mailAttachmentModel = new MailAttachment
                {
                    AttachId = mailAttachment.AttachId,
                    AttachFile = mailAttachment.AttachFile
                };
                mailInfo.Attachments.Add(mailAttachmentModel);
            }
            return mailInfo;

        }
        public IEnumerable<ModelView.InternalMailSelectUser> SearchUsers(IConnectionHandler connectionHandler, Guid currentUserId, string selectedIdlist, string value)
        {
            var list = new List<ModelView.InternalMailSelectUser>();
            var strings = string.IsNullOrEmpty(selectedIdlist) ? new[] { "" } : selectedIdlist.Split(',');
            var enterpriseNodeFacade = EnterpriseNodeComponent.Instance.EnterpriseNodeFacade;
            var otherModuleReal = enterpriseNodeFacade.Search(value);
            var otherModuleLegal = enterpriseNodeFacade.Search(value, EnterpriseNode.Tools.Enums.EnterpriseNodeType.LegalEnterPriseNode);
            var outlist = otherModuleReal.Union(otherModuleLegal);
            foreach (var enterpriseNode in outlist)
            {
                if (enterpriseNode.Id == currentUserId) continue;
                var selectUser = new ModelView.InternalMailSelectUser { Title = enterpriseNode.Title(), Id = enterpriseNode.Id, PhotoId = enterpriseNode.PictureId, IsSelcted = strings.Any(x => x.ToGuid() == enterpriseNode.Id) };
                list.Add(selectUser);
            }
            foreach (var s1 in strings)
            {
                if (list.Any(x => x.Id == s1.ToGuid())) continue;
                var enterpriseNode = enterpriseNodeFacade.Get(s1.ToGuid());
                if (enterpriseNode == null) continue;
                var selectUser = new ModelView.InternalMailSelectUser { Title = enterpriseNode.Title(), Id = enterpriseNode.Id, PhotoId = enterpriseNode.PictureId, IsSelcted = true };
                list.Add(selectUser);
            }
            return list;
        }

        public bool DeleteGroup(IConnectionHandler connectionHandler, string idlist)
        {
            if (string.IsNullOrEmpty(idlist)) return false;
            foreach (var variable in idlist.Split(','))
            {
                if (string.IsNullOrEmpty(variable)) continue;
                if (!this.Delete(connectionHandler, variable.ToGuid()))
                    throw new Exception("خطایی در حذف وجود دارد");
            }
            return true;
        }
    }
}
