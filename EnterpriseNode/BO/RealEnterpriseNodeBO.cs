using Radyn.EnterpriseNode.DataStructure;
using Radyn.Framework;
using Radyn.Framework.DbHelper;

namespace Radyn.EnterpriseNode.BO
{
    internal class RealEnterpriseNodeBO : BusinessBase<RealEnterpriseNode>
    {
        protected override void CheckConstraint(IConnectionHandler connectionHandler, RealEnterpriseNode item)
        {
            base.CheckConstraint(connectionHandler, item);
            if (item.NationalCode!=null&&!string.IsNullOrEmpty(item.NationalCode.Trim()) && !Utility.Utils.ValidNationalID(item.NationalCode))
                throw new  KnownException("لطفا کد ملی را به صورت صحیح وارد کنید");
        }
    }
}
