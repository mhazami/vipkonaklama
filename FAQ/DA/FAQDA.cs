using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.FAQ.DA
{
    public sealed class FAQDA : DALBase<FAQ.DataStructure.FAQ>
    {
        public FAQDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<DataStructure.FAQ> Search(string value)
        {
            var faqCommandBuilder = new FAQCommandBuilder();
            var query = faqCommandBuilder.Search(value);
            return DBManager.GetCollection<DataStructure.FAQ>(base.ConnectionHandler, query);
        }
    }
    internal class FAQCommandBuilder
    {
        public string Search(string value)
        {
            var where = "";
            var query =
                string.Format(
                    "SELECT     distinct    FAQ.FAQ.Id, FAQ.FAQ.Question, cast(FAQ.FAQ.Answer as nvarchar(4000)) as Answer , FAQ.FAQ.CreatorId, FAQ.FAQ.CreateDate, FAQ.FAQ.ViewCount, FAQ.FAQ.ThumbnailId, " +
                    "FAQ.FAQ.IsExternal FROM            FAQ.FAQ INNER JOIN " +
                    " FAQ.FAQContent ON FAQ.FAQ.Id = FAQ.FAQContent.Id ");
            if (!string.IsNullOrEmpty(value))
            {
                where += string.Format("FAQ.FAQContent.Question like N'%{0}%' or ", value);
                where += string.Format("FAQ.FAQContent.Answer like N'%{0}%' or ", value);
            }
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Substring(0, where.Length - 3);
                query = string.Format("{0} where {1} ", query, where);
            }
            return query + " order by CreateDate desc ";
        }
    }
}
