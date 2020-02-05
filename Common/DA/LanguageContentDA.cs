
using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Common.DA
{
public sealed class LanguageContentDA : DALBase<LanguageContent>
{
public LanguageContentDA(IConnectionHandler connectionHandler): base(connectionHandler)
{}

    public List<LanguageContent> GetByMoudelname(string key,string culture)
    {

        var builder = new LanguageContentCommandBuilder();
        var query = builder.GetByMoudelname(key, culture);
        return DBManager.GetCollection<LanguageContent>(base.ConnectionHandler, query);

    }
}
internal class LanguageContentCommandBuilder
{
    public string GetByMoudelname(string key, string culture)
    {
        
            return String.Format("Select distinct * from [Common].[LanguageContent] where [Key] like '%{0}%' and LanguageId='{1}' ", key, culture);
    }
}
}
