using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
public sealed class CityDA : DALBase<City>
{
public CityDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class CityCommandBuilder
{
}
}
