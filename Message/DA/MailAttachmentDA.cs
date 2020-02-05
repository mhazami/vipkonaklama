using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;

namespace Radyn.Message.DA
{
public sealed class MailAttachmentDA : DALBase<MailAttachment>
{
public MailAttachmentDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class MailAttachmentCommandBuilder
{
}
}
