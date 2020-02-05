using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Graphic.DataStructure;

namespace Radyn.Graphic.BO
{
    internal class ThemeBO : BusinessBase<Theme>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Theme obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
