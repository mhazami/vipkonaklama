using System.ComponentModel;

namespace Radyn.ModuleGeneretor
{
    public class Enums
    {
       public enum ModuleType
        {
            None = 0,
            [Description("پایه")]
            Base,
            [Description("مقالات")]
            Articles,
            [Description("تبلیغات")]
            Advertisements,
            [Description("پرسش/پاسخ")]
            FAQ,
            [Description("گالری")]
            Gallery,
            [Description("گرافیک")]
            Graphic,
            [Description("اخبار")]
            News,
            [Description("پرداخت")]
            Payment,
            [Description("دفترچه تلفن")]
            PhoneBook,
            [Description("اسلایدر")]
            Slider,
            [Description("آمار بازدید")]
            Statistics

        }

       public enum FileType
       {
           None = 0,
           [Description("dll")]
           Dll,
           [Description("asax")]
           asax,
           [Description("config")]
           config,
           [Description("aspx")]
           aspx,
           [Description("xml")]
           xml,
           [Description("png")]
           png,
           [Description("resx")]
           News,
           [Description("ttf")]
           ttf,
           [Description("cshtml")]
           cshtml,
         

       }
    }
}
