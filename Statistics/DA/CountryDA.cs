using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Statistics.DataStructure;

namespace Radyn.Statistics.DA
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
