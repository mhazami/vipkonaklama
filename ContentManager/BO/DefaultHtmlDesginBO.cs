using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Web.Parser;

namespace Radyn.ContentManager.BO
{
    internal class DefaultHtmlDesginBO : BusinessBase<DefaultHtmlDesgin>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DefaultHtmlDesgin obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }

        

        
    }
}
