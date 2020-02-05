using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsContentTypeDA : DALBase<NewsContentType>
{
public NewsContentTypeDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsContentTypeCommandBuilder
{
}
}
