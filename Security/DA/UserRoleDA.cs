using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
public sealed class UserRoleDA : DALBase<UserRole>
{
public UserRoleDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class UserRoleCommandBuilder
{
}
}
