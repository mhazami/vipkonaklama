using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsAttributeDA : DALBase<NewsAttribute>
{
public NewsAttributeDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsAttributeCommandBuilder
{
}
}
