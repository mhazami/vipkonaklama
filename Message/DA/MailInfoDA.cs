using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;

namespace Radyn.Message.DA
{
public sealed class MailInfoDA : DALBase<MailInfo>
{
public MailInfoDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class MailInfoCommandBuilder
{
}
}
