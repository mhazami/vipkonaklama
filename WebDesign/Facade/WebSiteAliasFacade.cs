using System;
using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.WebDesign.BO;
using Radyn.WebDesign.DataStructure;
using Radyn.WebDesign.Facade.Interface;

namespace Radyn.WebDesign.Facade
{
    internal sealed class WebSiteAliasFacade : WebDesignBaseFacade<WebSiteAlias>, IWebSiteAliasFacade
    {
        internal WebSiteAliasFacade()
        {
        }

        internal WebSiteAliasFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        {
        }

      


        public List<WebSiteAlias> GetByWebSiteId(Guid WebSiteId)
        {
            try
            {
                return new WebSiteAliasBO().GetByWebSiteId(this.ConnectionHandler, WebSiteId);
            }
            catch (Exception ex)
            {
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
