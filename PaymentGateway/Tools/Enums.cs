using System.ComponentModel;

namespace Radyn.PaymentGateway.Tools
{
    public class Enums
    {

        public enum Bank : byte
        {
            [Description("رادین")]
            Radyn = 1,
            [Description("بانک ملت")]
            Mellat = 2,
            [Description("بانک ملی")]
            Melli = 3,
            [Description("بانک پاسارگاد")]
            Pasargad = 4,
            [Description("بانک صادرات")]
            Saderat = 5,
            [Description("بانک سامان")]
            Saman = 6,
            [Description("بانک اقتصاد نوین")]
            EghtesadeNovin = 7,
            [Description("بانک تجارت")]
            IranKish = 8,
            [Description("مبنا کارت آریا")]
            Mabna = 9,
            [Description("بانک شهر و قوامین")]
            Ghavamin = 10,
            [Description("درگاه پرداخت زرین پال")]
            ZarinPal = 11,
        }


    }
}
