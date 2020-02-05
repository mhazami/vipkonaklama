using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class RelatedNewsDA : DALBase<RelatedNews>
{
public RelatedNewsDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class RelatedNewsCommandBuilder
{
}
}
