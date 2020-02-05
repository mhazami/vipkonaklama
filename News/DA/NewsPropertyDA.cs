using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsPropertyDA : DALBase<NewsProperty>
{
public NewsPropertyDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsPropertyCommandBuilder
{
}
}
