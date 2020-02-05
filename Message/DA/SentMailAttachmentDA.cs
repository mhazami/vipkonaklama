using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;

namespace Radyn.Message.DA
{
public sealed class SentMailAttachmentDA : DALBase<SentMailAttachment>
{
public SentMailAttachmentDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class SentMailAttachmentCommandBuilder
{
}
}
