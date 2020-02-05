using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsAttachedFileDA : DALBase<NewsAttachedFile>
{
public NewsAttachedFileDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsAttachedFileCommandBuilder
{
}
}
