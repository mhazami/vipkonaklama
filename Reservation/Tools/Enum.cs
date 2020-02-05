using Radyn.Framework;

namespace Radyn.Reservation
{
    public class Enum
    {
        public enum DayType : byte
        {
            [Description("NormalDay", Type = typeof(Resources.Reservation))]
            Normal = 1,
            [Description("Weekend", Type = typeof(Resources.Reservation))]
            Weekend = 2,
            [Description("Holiday", Type = typeof(Resources.Reservation))]
            Holiday = 3
        }
        public enum TimeSheet : byte
        {
            [Description("00:00")]
            Morning = 0,

            [Description("01:00")]
            oneAM = 1,

            [Description("02:00")]
            TowAM = 2,

            [Description("03:00")]
            ThreeAM = 3,

            [Description("04:00")]
            FourAM = 4,

            [Description("05:00")]
            FiveAM = 5,

            [Description("06:00")]
            SixAM = 6,

            [Description("07:00")]
            SevenAM = 7,

            [Description("08:00")]
            EightAM = 8,

            [Description("09:00")]
            NineAM = 9,

            [Description("10:00")]
            TenAM = 10,

            [Description("11:00")]
            ElevenAM = 11,

            [Description("12:00")]
            Noon = 12,

            [Description("13:00")]
            OnePM = 13,

            [Description("14:00")]
            TowPM = 14,

            [Description("15:00")]
            ThreePM = 15,

            [Description("16:00")]
            FourPM = 16,

            [Description("17:00")]
            FivePM = 17,

            [Description("18:00")]
            SixPM = 18,

            [Description("19:00")]
            SevenPM = 19,

            [Description("20:00")]
            EightPM = 20,

            [Description("21:00")]
            NinePM = 21,

            [Description("22:00")]
            TenPM = 22,

            [Description("23:00")]
            ElevenPM = 23,

        }

        public enum PaymentType : byte
        {
            None = 0,
            [Description("Cash", Type = typeof(Resources.Reservation))]
            Cash = 1,
            [Description("Credit", Type = typeof(Resources.Reservation))]
            Credit = 2,

        }


        public enum ReserveType : byte
        {
            None = 0,
            [Description("Short", Type = typeof(Resources.Reservation))]
            Short = 1,
            [Description("Night", Type = typeof(Resources.Reservation))]
            Night = 2,
            [Description("Daily", Type = typeof(Resources.Reservation))]
            Daily = 3
        }


        public enum DayOfWeek : byte
        {
            [Description("Sunday", Type = typeof(Resources.Reservation))]
            Sunday = 1,
            [Description("Monday", Type = typeof(Resources.Reservation))]
            Monday = 2,
            [Description("Tuesday", Type = typeof(Resources.Reservation))]
            Tuesday = 3,
            [Description("Wednesday", Type = typeof(Resources.Reservation))]
            Wednesday = 4,
            [Description("Thursday", Type = typeof(Resources.Reservation))]
            Thursday = 5,
            [Description("Friday", Type = typeof(Resources.Reservation))]
            Friday = 6,
            [Description("Saturday", Type = typeof(Resources.Reservation))]
            Saturday = 7
        }
    }
}
