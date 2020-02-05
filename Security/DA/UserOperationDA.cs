using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Security.DataStructure;

namespace Radyn.Security.DA
{
public sealed class UserOperationDA : DALBase<UserOperation>
{
public UserOperationDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class UserOperationCommandBuilder
{
}
}
