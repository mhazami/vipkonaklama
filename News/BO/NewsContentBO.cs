using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.News.DataStructure;

namespace Radyn.News.BO
{
    internal class NewsContentBO : BusinessBase<NewsContent>
    {
        public override bool Insert(IConnectionHandler connectionHandler, NewsContent obj)
        {
            if (obj.Lead.Contains("'"))
                obj.Lead = obj.Lead.Replace("'", "''");
            if (obj.Body.Contains("'"))
                obj.Body = obj.Body.Replace("'", "''");
            return base.Insert(connectionHandler, obj);
        }

        public override bool Update(IConnectionHandler connectionHandler, NewsContent obj)
        {
            if (obj.Lead.Contains("'"))
                obj.Lead = obj.Lead.Replace("'", "''");
            if (obj.Body.Contains("'"))
                obj.Body = obj.Body.Replace("'", "''");
            return base.Update(connectionHandler, obj);
        }
    }
}
