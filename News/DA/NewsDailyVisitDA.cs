using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsDailyVisitDA : DALBase<NewsDailyVisit>
{
public NewsDailyVisitDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsDailyVisitCommandBuilder
{
}
}
