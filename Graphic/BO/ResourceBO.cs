using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Graphic.DataStructure;

namespace Radyn.Graphic.BO
{
    internal class ResourceBO : BusinessBase<Resource>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Resource obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
