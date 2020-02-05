using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
public sealed class UserGroupDA : DALBase<UserGroup>
{
public UserGroupDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class UserGroupCommandBuilder
{
}
}
