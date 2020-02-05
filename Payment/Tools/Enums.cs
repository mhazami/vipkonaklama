
using Radyn.Framework;

namespace Radyn.Payment.Tools
{
    public class Enums
    {
        public enum PayType : byte
        {
            [Description("Documnet", Type = typeof(Resources.Payment))]
            Documnet = 1,
            [Description("OnlinePay", Type = typeof(Resources.Payment))]
            OnlinePay = 2,
        }


     
    }
}
