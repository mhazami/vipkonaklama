using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;
using Radyn.Message.Facade.Interface;

namespace Radyn.Message.Facade
{
    internal sealed class MailAttachmentFacade : MessageBaseFacade<MailAttachment>, IMailAttachmentFacade
    {
        internal MailAttachmentFacade() { }

        internal MailAttachmentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
