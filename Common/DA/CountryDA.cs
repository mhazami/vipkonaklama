using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
public sealed class CountryDA : DALBase<Country>
{
public CountryDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class CountryCommandBuilder
{
}
}
