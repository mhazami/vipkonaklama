using System;
using System.Collections.Generic;
using Radyn.ContentManager.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;


namespace Radyn.ContentManager.DA
{
    public sealed class PartialLoadDA : DALBase<PartialLoad>
    {
        public PartialLoadDA(IConnectionHandler connectionHandler)
            : base(connectionHandler)
        { }

        public IEnumerable<PartialLoad> GetForUpdatePostion(PartialLoad partialLoad, byte position)
        {
            var partialLoadCommandBuilder = new PartialLoadCommandBuilder();
            var query = partialLoadCommandBuilder.GetForUpdatePostion(partialLoad.HtmlDesginId, partialLoad.CustomId, position);
            return DBManager.GetCollection<PartialLoad>(base.ConnectionHandler, query);
        }
    }
    internal class PartialLoadCommandBuilder
    {
        public string GetForUpdatePostion(Guid htmlDesginId, string customId, byte position)
        {
            return
                string.Format(
                    "SELECT        *FROM      ContentManage.PartialLoad where ContentManage.PartialLoad.HtmlDesginId='{0}' and ContentManage.PartialLoad.CustomId='{1}' and ContentManage.PartialLoad.position >={2} ",
                    htmlDesginId, customId, position);
        }
    }
}
