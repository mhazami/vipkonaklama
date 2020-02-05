using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;

namespace Radyn.Statistics.DA
{
public sealed class AccountsDA : DALBase<Accounts>
{
public AccountsDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class AccountsCommandBuilder
{
}
}
