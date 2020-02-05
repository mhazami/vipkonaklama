using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.BO
{
internal class NewsCategoryBO : BusinessBase<NewsCategory>
{
    public override bool Insert(IConnectionHandler connectionHandler, NewsCategory obj)
    {
        var id = obj.Id;
        BOUtility.GetGuidForId(ref  id);
        obj.Id = id;
        return base.Insert(connectionHandler, obj);
    }
}
}
