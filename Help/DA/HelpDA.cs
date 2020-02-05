using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Help.DA
{
public sealed class HelpDA : DALBase<Help.DataStructure.Help>
{
public HelpDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}

    public IEnumerable<DataStructure.Help> Search(string txt)
    {
        var helpCommandBuilder = new HelpCommandBuilder();
        var query = helpCommandBuilder.Search(txt);
        return DBManager.GetCollection<DataStructure.Help>(base.ConnectionHandler, query);
    }
}
internal class HelpCommandBuilder
{
    public string Search(string txt)
    {
        var query = "SELECT      Help.Help.*" +
                    " FROM         Help.Help inner JOIN " +
                    " Help.HelpContent ON Help.Help.Id = Help.HelpContent.HelpId";
        string where = "";
        if (!string.IsNullOrEmpty(txt))
        {
            where += string.Format("Help.DefaultTitle like N'%{0}%' or ", txt);
            where += string.Format("Help.DefaultConent like N'%{0}%' or ", txt);
            where += string.Format("HelpContent.Content like N'%{0}%' or ", txt);
            where += string.Format("HelpContent.Title like N'%{0}%' or ", txt);
        }
        if (!string.IsNullOrEmpty(where))
        {
            where = where.Substring(0, where.Length - 3);
            return string.Format("{0} where {1}  order by HelpContent.CreateDate desc ", query, where);
        }
        return query;
    }
}
}
