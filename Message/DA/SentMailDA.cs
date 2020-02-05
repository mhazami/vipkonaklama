using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Message.DataStructure;

namespace Radyn.Message.DA
{
public sealed class SentMailDA : DALBase<SentMail>
{
public SentMailDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class SentMailCommandBuilder
{
}
}
