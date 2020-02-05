using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.DA
{
public sealed class NewsCategoryDA : DALBase<NewsCategory>
{
public NewsCategoryDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}
}
internal class NewsCategoryCommandBuilder
{
}
}
