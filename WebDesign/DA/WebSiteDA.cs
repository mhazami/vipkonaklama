using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.WebDesign.DataStructure;

namespace Radyn.WebDesign.DA
{
    public sealed class WebSiteDA : DALBase<WebSite>
    {
        public WebSiteDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }


        public bool ExecureQuery(List<string> querylist)
        {
            
            foreach (var VARIABLE in querylist)
            {
                DBManager.ExecuteNonQuery(this.ConnectionHandler, VARIABLE);
            }

            return true;
        }
    }
    internal class WebSiteCommandBuilder
    {
       
    }
}

