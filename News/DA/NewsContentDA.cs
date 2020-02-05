using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsContentDA : DALBase<NewsContent>
{
public NewsContentDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsContentCommandBuilder
{
}
}
