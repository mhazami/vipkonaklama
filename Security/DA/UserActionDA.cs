using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
public sealed class UserActionDA : DALBase<UserAction>
{
public UserActionDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class UserActionCommandBuilder
{
}
}
