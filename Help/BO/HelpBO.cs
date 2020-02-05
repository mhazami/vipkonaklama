using System.Collections.Generic;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Help.DA;

namespace Radyn.Help.BO
{
    internal class HelpBO : BusinessBase<Help.DataStructure.Help>
    {
        public override bool Insert(IConnectionHandler connectionHandler, DataStructure.Help obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
      
        public IEnumerable<DataStructure.Help> Search(IConnectionHandler connectionHandler, string txt)
        {
            var da = new HelpDA(connectionHandler);
            return da.Search(txt);
        }
    }
}
