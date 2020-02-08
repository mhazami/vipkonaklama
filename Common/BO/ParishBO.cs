using Radyn.Common.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using System;

namespace Radyn.Common.BO
{
    internal class ParishBO : BusinessBase<Parish>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Parish obj)
        {
            Guid id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
