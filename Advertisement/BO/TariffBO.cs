using Radyn.Advertisements.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.Advertisements.BO
{
    internal class TariffBO : BusinessBase<Tariff>
    {
        public override bool Insert(IConnectionHandler connectionHandler, Tariff obj)
        {
            var id = obj.Id;
            BOUtility.GetGuidForId(ref  id);
            obj.Id = id;
            return base.Insert(connectionHandler, obj);
        }
    }
}
