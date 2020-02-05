using Radyn.Framework;

namespace Radyn.Common.Definition
{
    public class Enums
    {
        public enum PersianDay : byte
        {
            [Description("Saturday", Type = typeof(Resources.Common))]
            Saturday = 1,
            [Description("SunDay", Type = typeof(Resources.Common))]
            SunDay = 2,
            [Description("Monday", Type = typeof(Resources.Common))]
            Monday = 3,
            [Description("Thersday", Type = typeof(Resources.Common))]
            Thersday = 4,
            [Description("Wensday", Type = typeof(Resources.Common))]
            Wensday = 5,
            [Description("Thurday", Type = typeof(Resources.Common))]
            Thurday = 6,
            [Description("Friday", Type = typeof(Resources.Common))]
            Friday = 7
        }
        public enum PersianMonth : byte
        {
            [Description("Farvardin", Type = typeof(Resources.Common))]
            Farvardin = 1,
            [Description("Ordibehesht", Type = typeof(Resources.Common))]
            Ordibehesht = 2,
            [Description("Khordad", Type = typeof(Resources.Common))]
            Khordad = 3,
            [Description("Tir", Type = typeof(Resources.Common))]
            Tir = 4,
            [Description("Mordad", Type = typeof(Resources.Common))]
            Mordad = 5,
            [Description("Sharhrivar", Type = typeof(Resources.Common))]
            Sharhrivar = 6,
            [Description("Mehr", Type = typeof(Resources.Common))]
            Mehr = 7,
            [Description("Aban", Type = typeof(Resources.Common))]
            Aban = 8,
            [Description("Azar", Type = typeof(Resources.Common))]
            Azar = 9,
            [Description("Day", Type = typeof(Resources.Common))]
            Day = 10,
            [Description("Bahman", Type = typeof(Resources.Common))]
            Bahman = 11,
            [Description("Esfand", Type = typeof(Resources.Common))]
            Esfand = 12
        }
        public enum PersianWeek : byte
        {
            [Description("FirstWeek", Type = typeof(Resources.Common))]
            FirstWeek = 1,
            [Description("SecondWeek", Type = typeof(Resources.Common))]
            SecondWeek = 2,
            [Description("ThirdWeek", Type = typeof(Resources.Common))]
            ThirdWeek = 3,
            [Description("FourWeek", Type = typeof(Resources.Common))]
            FourWeek = 4,
            [Description("FiveWeek", Type = typeof(Resources.Common))]
            FiveWeek = 5,

        }
        public enum CurrencyType : byte
        {
            [Description("Rial", Type = typeof(Resources.Common))]
            Rial = 0,
            [Description("Dolar", Type = typeof(Resources.Common))]
            Dolar = 1,
            [Description("Euro", Type = typeof(Resources.Common))]
            Euro = 2,
          
        }
        public enum PersianSeason : byte
        {
            [Description("Spring", Type = typeof(Resources.Common))]
            Spring = 1,
            [Description("Summer", Type = typeof(Resources.Common))]
            Summer = 2,
            [Description("Autumn", Type = typeof(Resources.Common))]
            Autumn,
            [Description("Winter", Type = typeof(Resources.Common))]
            Winter = 4,

        }

    }
}
