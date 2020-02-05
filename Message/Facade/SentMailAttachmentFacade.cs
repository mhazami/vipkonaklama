using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;
using Radyn.Message.Facade.Interface;

namespace Radyn.Message.Facade
{
    internal sealed class SentMailAttachmentFacade : MessageBaseFacade<SentMailAttachment>, ISentMailAttachmentFacade
    {
        internal SentMailAttachmentFacade() { }

        internal SentMailAttachmentFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

       
    }
}
