using System;

namespace Radyn.Reservation.Tools
{
    public class Utility
    {
        public int GetWeekendCount(DateTime startDate, DateTime endDate)
        {
            int result = 0;

            int days = GetDaysCount(startDate, endDate);
            for (int i = 0; i <= days - 1; i++)
            {
                DateTime testDate = startDate.AddDays(i);
                switch (testDate.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        result++;
                        break;
                }
            }
            return result;
        }

        public int GetWeekendCount(string startDate, string endDate)
        {
            int result = 0;
            int[] startDateArray = SplitStringDate(startDate);
            DateTime start = new DateTime(startDateArray[0], startDateArray[1], startDateArray[2]);

            int days = GetDaysCount(startDate, endDate);
            for (int i = 0; i <= days - 1; i++)
            {
                DateTime testDate = start.AddDays(i);
                switch (testDate.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        result++;
                        break;
                }
            }
            return result;
        }

        private int[] SplitStringDate(string date)
        {
            int[] result = new int[3];
            string[] list = date.Split('-');
            for (int i = 0; i < list.Length; i++)
            {
                result[i] = int.Parse(list[i]);
            }
            return result;
        }

        public int GetDaysCount(DateTime startDate, DateTime endDate)
        {
            TimeSpan diff = endDate - startDate;
            return diff.Days;
        }

        public int GetDaysCount(string startDate, string endDate)
        {
            int[] startDateArray = SplitStringDate(startDate);
            int[] endDateArray = SplitStringDate(endDate);
            DateTime start = new DateTime(startDateArray[0], startDateArray[1], startDateArray[2]);
            DateTime end = new DateTime(endDateArray[0], endDateArray[1], endDateArray[2]);
            TimeSpan diff = end - start;
            return diff.Days == 0 ? 1 : diff.Days;
        }

    }

    public static class DateTimeUtility
    {
        public static string GetDateString(this DateTime date)
        {
            return string.Format("{0}-{1:D2}-{2:D2}", date.Year, date.Month, date.Day);
        }

        public static string MonthName(this DateTime date, string culture)
        {
            string res = string.Empty;
            int m = date.Month;
            if (culture == "fa-IR")
            {
                switch (m)
                {
                    case 1:
                        res = "فروردین";
                        break;
                    case 2:
                        res = "اردیبهشت";
                        break;
                    case 3:
                        res = "خرداد";
                        break;
                    case 4:
                        res = "تیر";
                        break;
                    case 5:
                        res = "مرداد";
                        break;
                    case 6:
                        res = "شهریور";
                        break;
                    case 7:
                        res = "مهر";
                        break;
                    case 8:
                        res = "آبان";
                        break;
                    case 9:
                        res = "آذر";
                        break;
                    case 10:
                        res = "دی";
                        break;
                    case 11:
                        res = "بهمن";
                        break;
                    case 12:
                        res = "اسفند";
                        break;
                    default:
                        res = "";
                        break;
                }
            }
            else if (culture == "tr-TR")
            {
                switch (m)
                {
                    case 1:
                        res = "Ocak";
                        break;
                    case 2:
                        res = "Şubat";
                        break;
                    case 3:
                        res = "Mart";
                        break;
                    case 4:
                        res = "Nisan";
                        break;
                    case 5:
                        res = "Mayıs";
                        break;
                    case 6:
                        res = "Haziran";
                        break;
                    case 7:
                        res = "Temmuz";
                        break;
                    case 8:
                        res = "Ağustos";
                        break;
                    case 9:
                        res = "Eylül";
                        break;
                    case 10:
                        res = "Ekim";
                        break;
                    case 11:
                        res = "Kasım";
                        break;
                    case 12:
                        res = "Aralık";
                        break;
                    default:
                        res = "";
                        break;
                }
            }
            else
            {
                switch (m)
                {
                    case 1:
                        res = "January";
                        break;
                    case 2:
                        res = "February";
                        break;
                    case 3:
                        res = "March";
                        break;
                    case 4:
                        res = "April";
                        break;
                    case 5:
                        res = "May";
                        break;
                    case 6:
                        res = "June";
                        break;
                    case 7:
                        res = "July";
                        break;
                    case 8:
                        res = "August";
                        break;
                    case 9:
                        res = "September";
                        break;
                    case 10:
                        res = "October";
                        break;
                    case 11:
                        res = "November";
                        break;
                    case 12:
                        res = "December";
                        break;
                    default:
                        res = "";
                        break;
                }
            }

            return res;
        }

        public static string DayOfWeekName(this DateTime date, string culture)
        {
            string res = string.Empty;
            DayOfWeek m = date.DayOfWeek;
            if (culture == "fa-IR")
            {
                switch (m)
                {
                    case DayOfWeek.Sunday:
                        res = "یکشنبه";
                        break;
                    case DayOfWeek.Monday:
                        res = "دوشنبه";
                        break;
                    case DayOfWeek.Tuesday:
                        res = "سه شنبه";
                        break;
                    case DayOfWeek.Wednesday:
                        res = "چهارشنبه";
                        break;
                    case DayOfWeek.Thursday:
                        res = "پنجشنبه";
                        break;
                    case DayOfWeek.Friday:
                        res = "جمعه";
                        break;
                    case DayOfWeek.Saturday:
                        res = "شنبه";
                        break;
                    default:
                        return "";
                }
            }
            else if (culture == "tr-TR")
            {
                switch (m)
                {
                    case DayOfWeek.Sunday:
                        res = "Pazar";
                        break;
                    case DayOfWeek.Monday:
                        res = "Pazartesi";
                        break;
                    case DayOfWeek.Tuesday:
                        res = "Salı";
                        break;
                    case DayOfWeek.Wednesday:
                        res = "Çarsamba";
                        break;
                    case DayOfWeek.Thursday:
                        res = "Persembe";
                        break;
                    case DayOfWeek.Friday:
                        res = "Cuma";
                        break;
                    case DayOfWeek.Saturday:
                        res = "Cumartesi";
                        break;
                    default:
                        return "";
                }
            }
            else
            {
                switch (m)
                {
                    case DayOfWeek.Sunday:
                        res = "Sunday";
                        break;
                    case DayOfWeek.Monday:
                        res = "Monday";
                        break;
                    case DayOfWeek.Tuesday:
                        res = "Tuesday";
                        break;
                    case DayOfWeek.Wednesday:
                        res = "Wednesday";
                        break;
                    case DayOfWeek.Thursday:
                        res = "Thursday";
                        break;
                    case DayOfWeek.Friday:
                        res = "Friday";
                        break;
                    case DayOfWeek.Saturday:
                        res = "Saturday";
                        break;
                    default:
                        return "";
                }
            }

            return res;
        }
    }
}
