using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
public sealed class BankDA : DALBase<Bank>
{
public BankDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class BankCommandBuilder
{
}
}
