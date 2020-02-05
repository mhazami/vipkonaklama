using Radyn.Framework;

namespace Radyn.EnterpriseNode.Tools
{
    public class Enums
    {
        public enum EnterpriseNodeType : byte
        {
            None=0,
            [Description("RealEnterPriseNode", Type = typeof(Resources.EnterpriseNode))]
            RealEnterPriseNode = 1,
            [Description("LegalEnterPriseNode", Type = typeof(Resources.EnterpriseNode))]
            LegalEnterPriseNode = 2,

        }
        public enum Gender : byte
        {

            None = 0,
            [Description("Man", Type = typeof(Resources.EnterpriseNode))]
            Man = 1,
            [Description("Women", Type = typeof(Resources.EnterpriseNode))]
            Women = 2,

        }
    }
}
